using Helios.Messages;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Item;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Game
{
    public class TeleporterInteractor : Interactor
    {
        public const string TELEPORTER_CLOSE = "0";
        public const string TELEPORTER_OPEN = "1";
        public const string TELEPORTER_EFFECTS = "2";

        #region Overridden Properties


        #endregion

        public TeleporterInteractor(Item item) : base(item)
        {
        }

        public override void WriteExtraData(IMessageComposer composer, bool inventoryView = false)
        {
            var data = GetJsonObject<TeleporterExtraData>();
            /*
            composer.Data.Add((int)ExtraDataType.Legacy);
            composer.Data.Add(data.State ?? "0");*/

            composer.AppendStringWithBreak(data.State ?? "0");
        }

        public override void OnInteract(IEntity entity, int requestData)
        {
            var roomUser = entity.RoomEntity;
            var room = entity.RoomEntity.Room;

            if (!string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                return;

            Position front = Item.Position.GetSquareInFront();

            if (front != roomUser.Position && Item.Position != roomUser.Position)
            {
                roomUser.Move(front.X, front.Y);
                return;
            }

            var teleporterData = GetJsonObject<TeleporterExtraData>();
            string pairId = teleporterData.LinkedItem;

            ItemData targetTeleporterData = null;

            using (var context = new StorageContext())
            {
                context.GetItem(pairId);
            }

            Item.UpdateState(TELEPORTER_OPEN);

            roomUser.Move(Item.Position.X, Item.Position.Y);
            roomUser.WalkingAllowed = false;

            // Broken link, make user walk in then walk out
            if (string.IsNullOrEmpty(pairId) || targetTeleporterData == null || targetTeleporterData.RoomId == null || RoomManager.Instance.GetRoom(targetTeleporterData.RoomId.Value) == null)
            {
                Task.Delay(1000).ContinueWith(t =>
                {
                    roomUser.Move(
                        Item.Position.GetSquareInFront().X, 
                        Item.Position.GetSquareInFront().Y
                    );
                });


                Task.Delay(2000).ContinueWith(t =>
                {
                    Item.UpdateState(TELEPORTER_CLOSE);
                });

                Task.Delay(2500).ContinueWith(t =>
                {
                    roomUser.WalkingAllowed = true;
                });

                return;
            }

            var targetTeleporter = ItemManager.Instance.ResolveItem(targetTeleporterData.Id.ToString());
            var pairedTeleporter = targetTeleporter ?? new Item(targetTeleporterData);
            var pairedData = pairedTeleporter.Interactor.GetJsonObject<TeleporterExtraData>();

            roomUser.AuthenticateTeleporterId = pairedTeleporter.Data.Id.ToString();

            // Check if the user is inside the teleporter, if so, walk out. Useful if the user is stuck inside.
            if (Item.Position == roomUser.Position &&
                !RoomTile.IsValidTile(roomUser.Room, entity, Item.Position.GetSquareInFront()))
            {
                Item.UpdateState(TELEPORTER_EFFECTS);

                Task.Delay(1000).ContinueWith(t =>
                {
                    if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                        return;

                    Item.UpdateState(TELEPORTER_CLOSE);
                });

                Task.Delay(2000).ContinueWith(t =>
                {
                    if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                        return;

                    if (pairedTeleporter.Data.RoomId == Item.Data.RoomId)
                    {
                        pairedTeleporter.UpdateState(TELEPORTER_EFFECTS);

                        var newPosition = pairedTeleporter.Position.Copy();
                        newPosition.Rotation = pairedTeleporter.Position.Rotation;

                        roomUser.Warp(newPosition, instantUpdate: true);
                    }
                    else
                    {
                        roomUser.AuthenticateRoomId = pairedTeleporter.Data.RoomId;
                        roomUser.Room.Forward(roomUser.Entity);
                    }
                });

                // Handle teleporting in the same room
                if (pairedTeleporter.Data.RoomId == Item.Data.RoomId)
                {
                    Task.Delay(3000).ContinueWith(t =>
                    {
                        if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                            return;

                        pairedTeleporter.UpdateState(TELEPORTER_OPEN);

                        roomUser.Move(
                            pairedTeleporter.Position.GetSquareInFront().X,
                            pairedTeleporter.Position.GetSquareInFront().Y);

                        roomUser.WalkingAllowed = true;
                    });

                    Task.Delay(4000).ContinueWith(t =>
                    {
                        if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                            return;

                        pairedTeleporter.UpdateState(TELEPORTER_CLOSE);
                        roomUser.AuthenticateTeleporterId = null;
                    });

                }

                return;
            }

            // Resume normal teleportation
            Task.Delay(1000).ContinueWith(t =>
            {
                if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                    return;

                Item.UpdateState(TELEPORTER_EFFECTS);
            });

            Task.Delay(1500).ContinueWith(t =>
            {
                if (string.IsNullOrEmpty(roomUser.AuthenticateTeleporterId))
                    return;

                Item.UpdateState(TELEPORTER_CLOSE);

                if (pairedTeleporter.Data.RoomId != Item.Data.RoomId)
                {
                    roomUser.AuthenticateRoomId = pairedTeleporter.Room.Data.Id;
                    pairedTeleporter.Room.Forward(roomUser.Entity);
                }
                else
                {
                    roomUser.Warp(pairedTeleporter.Position.Copy(), instantUpdate: true);
                }

                if (pairedTeleporter.Data.RoomId == Item.Data.RoomId)
                {
                    pairedTeleporter.UpdateState(TELEPORTER_EFFECTS);
                }
            });

            if (pairedTeleporter.Data.RoomId == Item.Data.RoomId)
            {
                Task.Delay(3000).ContinueWith(t =>
                {
                    if (roomUser.RoomId != room.Data.Id)
                    {
                        roomUser.AuthenticateTeleporterId = null;
                        return;
                    }

                    pairedTeleporter.UpdateState(TELEPORTER_OPEN);

                    roomUser.WalkingAllowed = true;
                    roomUser.Move(
                        pairedTeleporter.Position.GetSquareInFront().X,
                        pairedTeleporter.Position.GetSquareInFront().Y);
                });

                Task.Delay(4000).ContinueWith(t =>
                {
                    if (roomUser.RoomId != room.Data.Id)
                    {
                        roomUser.AuthenticateTeleporterId = null;
                        return;
                    }

                    roomUser.AuthenticateTeleporterId = null;

                    if (pairedTeleporter.Data.RoomId == Item.Data.RoomId)
                    {
                        pairedTeleporter.UpdateState(TELEPORTER_CLOSE);
                    }
                    else
                    {
                        pairedTeleporter.UpdateState(TELEPORTER_CLOSE);
                    }
                });
            }
        }

        public override void OnPickup(IEntity entity)
        {
            var teleporterData = GetJsonObject<TeleporterExtraData>();

            Item.Interactor.SetExtraData(new TeleporterExtraData
            {
                LinkedItem = teleporterData.LinkedItem,
                State = TELEPORTER_CLOSE
            });

            Item.Save();
        }

        public override void OnPlace(IEntity entity)
        {
            var teleporterData = GetJsonObject<TeleporterExtraData>();

            Item.Interactor.SetExtraData(new TeleporterExtraData
            {
                LinkedItem = teleporterData.LinkedItem,
                State = TELEPORTER_CLOSE
            });

            Item.Save();
        }
    }
}
