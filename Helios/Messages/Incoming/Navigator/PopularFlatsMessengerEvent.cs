using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    public class PopularFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var roomList = RoomManager.Instance.ReplaceQueryRooms(
                RoomDao.GetPopularFlats()
            );

            avatar.Send(new FlatListComposer(2, roomList, NavigatorManager.Instance.GetPopularPromotion()));
        }
    }
}
