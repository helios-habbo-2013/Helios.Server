using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming.Catalogue
{
    class CatalogueClubGiftsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Subscription.Refresh();
            player.Subscription.CountMemberDays();

            player.Send(new CatalogueClubGiftsMessageComposer(player.IsSubscribed ? player.Subscription : null, SubscriptionManager.Instance.Gifts));
        }
    }
}
