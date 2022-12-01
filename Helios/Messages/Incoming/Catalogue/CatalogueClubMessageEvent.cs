using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming.Catalogue
{
    class CatalogueClubMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new CatalogueClubMessageComposer(SubscriptionManager.Instance.Subscriptions));
        }
    }
}
