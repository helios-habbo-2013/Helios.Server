using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    public class SearchFlatTagsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var searchQuery = request.ReadString().FilterInput(true);

            using (var context = new StorageContext())
            {
                var roomList = RoomManager.SortRooms(
                    RoomManager.Instance.ReplaceQueryRooms(context.SearchTags(searchQuery))
                );

                avatar.Send(new FlatListComposer(1, 9, "", roomList));
            }
        }

        public int HeaderId => 438;
    }
}
