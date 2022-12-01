using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class PopularTagsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new PopularTagsComposer(TagDao.GetPopularTags()));
        }
    }
}
