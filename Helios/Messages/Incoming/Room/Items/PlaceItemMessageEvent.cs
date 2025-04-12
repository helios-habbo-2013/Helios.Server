using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    class PlaceItemMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;
            var placementData = request.ReadString().Split(' ');

            if (!placementData[0].IsNumeric())
                return;

            int itemId = int.Parse(placementData[0]);
            Item item = avatar.Inventory.GetItem(itemId);

            if (item == null)
                return;

            if (room == null || !room.RightsManager.HasRights(avatar.Details.Id) || (room.ItemManager.HasItem(x => x.Definition.HasBehaviour(ItemBehaviour.STICKY_POLE)) && item.Definition.InteractorType == InteractorType.POST_IT)) 
            {
                avatar.Send(new ItemPlaceErrorComposer(ItemPlaceError.NoRights));
                return;
            }


            if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                // Do nothing if dimmer exists.. replicating Habbo's behaviour here, I literally bought another room dimmer on official Habbo just to test what happens!
                if (item.Definition.InteractorType == InteractorType.ROOMDIMMER &&
                    room.ItemManager.HasItem(x => x.Definition.InteractorType == InteractorType.ROOMDIMMER))
                    return;

                var wallPosition = $"{placementData[1]} {placementData[2]} {placementData[3]}";
                room.FurnitureManager.AddItem(item, wallPosition: wallPosition, avatar: avatar);
            }
            else
            {
                int x = (int)double.Parse(placementData[1]);
                int y = (int)double.Parse(placementData[2]);
                int rotation = (int)double.Parse(placementData[3]);

                var position = new Position();
                position.X = x;
                position.Y = y;
                position.Rotation = rotation;

                if (!item.IsValidMove(item, room, x, y, rotation))
                {
                    avatar.Send(new ItemPlaceErrorComposer(ItemPlaceError.NoPlacementAllowed));
                    return;
                }

                room.FurnitureManager.AddItem(item, position, avatar: avatar);
            }

            avatar.Inventory.RemoveItem(item);
        }

        public int HeaderId => -1;
    }
}
