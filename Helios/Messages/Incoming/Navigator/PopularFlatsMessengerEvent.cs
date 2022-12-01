using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Incoming
{
    public class PopularFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var roomList = RoomManager.Instance.ReplaceQueryRooms(
                RoomDao.GetPopularFlats()
            );

            player.Send(new FlatListComposer(2, roomList, NavigatorManager.Instance.GetPopularPromotion()));
        }
    }
}
