using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    class GetPromotableRoomsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.Instance.ReplaceQueryRooms(RoomDao.GetUserRooms(player.Details.Id));
            player.Send(new PromotableRoomsMessageComposer(roomList));
        }
    }
}
