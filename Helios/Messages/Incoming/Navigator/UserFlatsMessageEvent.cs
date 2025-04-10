using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    public class UserFlatsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new StorageContext())
            {
                var roomList = RoomManager.SortRooms(
                    RoomManager.Instance.ReplaceQueryRooms(context.GetUserRooms(avatar.Details.Id))
                );

                avatar.Send(new FlatListComposer(2, roomList, null));
            }
        }
    }
}
