using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Util;

namespace Helios.Messages.Incoming.Catalogue
{
    class ChoseClubGiftMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var subscriptionGift = SubscriptionManager.Instance.GetGift(request.ReadString());

            if (avatar.IsSubscribed)
                avatar.Subscription.Refresh();

            if (subscriptionGift == null || !avatar.IsSubscribed || avatar.Subscription.Data.GiftsRedeemable <= 0)
                return;

            if (!subscriptionGift.IsGiftRedeemable(avatar.Subscription.Data.SubscriptionAge))
                return;

            avatar.Subscription.Data.GiftsRedeemable--;

            using (var context = new StorageContext())
            {
                context.SaveGiftsRedeemable(avatar.Details.Id, avatar.Subscription.Data.GiftsRedeemable);
            }

            CatalogueManager.Instance.Purchase(avatar.Details.Id, subscriptionGift.CatalogueItem.Data.Id, 1, string.Empty, DateUtil.GetUnixTimestamp(), isClubGift: true);
        }

        public int HeaderId => -1;
    }
}
