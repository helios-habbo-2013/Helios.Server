using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class GetPromotableRoomsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new StorageContext())
            {
                var roomList = RoomManager.Instance.ReplaceQueryRooms(context.GetUserRooms(avatar.Details.Id));
                avatar.Send(new PromotableRoomsMessageComposer(roomList));
            }
        }

        public int HeaderId => -1;
    }
}
