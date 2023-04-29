using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming.Catalogue
{
    class CatalogueClubGiftsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Subscription.Refresh();
            avatar.Subscription.CountMemberDays();

            avatar.Send(new CatalogueClubGiftsMessageComposer(avatar.IsSubscribed ? avatar.Subscription : null, SubscriptionManager.Instance.Gifts));
        }
    }
}
