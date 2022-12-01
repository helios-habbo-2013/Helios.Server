using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class UserFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.SortRooms(
                RoomManager.Instance.ReplaceQueryRooms(RoomDao.GetUserRooms(player.Details.Id))
            );

            player.Send(new FlatListComposer(2, roomList, null));
        }
    }
}
