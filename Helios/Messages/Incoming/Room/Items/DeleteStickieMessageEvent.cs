using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class DeleteStickieMessageEvent : IMessageEvent
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

            if (item == null && item.Data.OwnerId != avatar.Details.Id && !room.RightsManager.IsOwner(avatar.Details.Id)) // TODO: Staff check
                return;

            room.FurnitureManager.RemoveItem(item, avatar);

            using (var context = new GameStorageContext())
            {
                context.DeleteItem(item.Data);
            }
        }
    }
}
