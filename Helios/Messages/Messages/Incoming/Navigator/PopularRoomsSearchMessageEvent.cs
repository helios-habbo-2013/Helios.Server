using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using static Helios.Game.FuserightManager;

namespace Helios.Messages.Incoming
{
    class PopularRoomsSearchMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new StorageContext())
            {
                var roomList = RoomManager.Instance.ReplaceQueryRooms(
                    context.GetPopularFlats()
                );

                avatar.Send(new GuestRoomSearchResultComposer(0, 2, "", roomList));
            }
        }

        public int HeaderId => 430;
    }
}