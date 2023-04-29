using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class OpenCatalogueMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new CataloguePagesComposer(avatar.Details.Rank, avatar.IsSubscribed));
        }
    }
}
