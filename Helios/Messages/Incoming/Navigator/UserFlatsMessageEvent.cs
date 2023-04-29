using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class UserFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var roomList = RoomManager.SortRooms(
                RoomManager.Instance.ReplaceQueryRooms(RoomDao.GetUserRooms(avatar.Details.Id))
            );

            avatar.Send(new FlatListComposer(2, roomList, null));
        }
    }
}
