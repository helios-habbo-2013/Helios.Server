using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class SearchFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.SortRooms(
                RoomManager.Instance.ReplaceQueryRooms(RoomDao.SearchRooms(request.ReadString()))
            );

            player.Send(new FlatListComposer(2, roomList, null));
        }
    }
}
