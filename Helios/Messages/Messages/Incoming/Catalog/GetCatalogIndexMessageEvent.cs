using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetCatalogIndexMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new CatalogIndexMessageComposer(avatar.Details.Rank, avatar.IsSubscribed));
        }


        public int HeaderId => 101;
    }
}