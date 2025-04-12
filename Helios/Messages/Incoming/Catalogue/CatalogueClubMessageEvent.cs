using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming.Catalogue
{
    class CatalogueClubMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new CatalogueClubMessageComposer(SubscriptionManager.Instance.Subscriptions));
        }

        public int HeaderId => -1;
    }
}
