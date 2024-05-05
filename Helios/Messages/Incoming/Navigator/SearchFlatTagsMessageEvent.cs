using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    public class SearchFlatTagsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var roomList = RoomManager.SortRooms(
                RoomManager.Instance.ReplaceQueryRooms(RoomDao.SearchTags(request.ReadString()))
            );

            avatar.Send(new FlatListComposer(2, roomList, null));
        }
    }
}
