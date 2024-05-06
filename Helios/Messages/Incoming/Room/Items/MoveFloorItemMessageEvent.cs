using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class MoveFloorItemMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int itemId = request.ReadInt();

            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null)
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null || !room.RightsManager.HasRights(avatar.Details.Id))
                return;

            int x = request.ReadInt();
            int y = request.ReadInt();
            int rotation = request.ReadInt();

            var oldPosition = item.Position.Copy();

            bool isRotation = false;

            if (item.Position != new Position(x, y) && item.Position.Rotation != rotation)
            {
                isRotation = true;
            }

            if (isRotation)
            {
                if (item.RollingData != null)
                {
                    return; // Don't allow rotating when rolling.
                }
            }

            if ((oldPosition.X == x &&
                oldPosition.Y == y &&
                oldPosition.Rotation == rotation) || !item.IsValidMove(item, room, x, y, rotation))
            {
                if (new Position(x, y) != item.Position)
                    room.Send(new UpdateFloorItemComposer(item));

                return;
            }

            room.FurnitureManager.MoveItem(item, new Position
            {
                X = x,
                Y = y,
                Rotation = rotation
            });
        }
    }
}
