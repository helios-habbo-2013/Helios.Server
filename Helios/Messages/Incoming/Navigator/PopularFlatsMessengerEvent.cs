using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    public class PopularFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new StorageContext())
            {
                var roomList = RoomManager.Instance.ReplaceQueryRooms(
                    context.GetPopularFlats()
                );

                avatar.Send(new FlatListComposer(2, roomList, NavigatorManager.GetPopularPromotion()));
            }
        }
    }
}
