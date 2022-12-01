﻿using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;
using Helios.Util;

namespace Helios.Messages.Incoming.Catalogue
{
    class ChoseClubGiftMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var subscriptionGift = SubscriptionManager.Instance.GetGift(request.ReadString());

            if (player.IsSubscribed)
                player.Subscription.Refresh();

            if (subscriptionGift == null || !player.IsSubscribed || player.Subscription.Data.GiftsRedeemable <= 0)
                return;

            if (!subscriptionGift.IsGiftRedeemable(player.Subscription.Data.SubscriptionAge))
                return;

            player.Subscription.Data.GiftsRedeemable--;

            SubscriptionDao.SaveGiftsRedeemable(player.Details.Id, player.Subscription.Data.GiftsRedeemable);
            CatalogueManager.Instance.Purchase(player.Details.Id, subscriptionGift.CatalogueItem.Data.Id, 1, string.Empty, DateUtil.GetUnixTimestamp(), isClubGift: true);
        }
    }
}
