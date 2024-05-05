using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    public class PopularTagsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new PopularTagsComposer(TagDao.GetPopularTags()));
        }
    }
}
