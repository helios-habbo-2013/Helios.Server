using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class MoveWallItemMessageEvent : IMessageEvent
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

            if (item == null || item.Data.OwnerId != avatar.Details.Id) // TODO: Staff check
                return;

            string wallPosition = request.ReadString();
            room.FurnitureManager.MoveItem(item, wallPosition: wallPosition);
        }
    }
}
