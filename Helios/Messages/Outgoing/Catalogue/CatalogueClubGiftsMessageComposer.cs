﻿using System;
using System.Collections.Generic;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class CatalogueClubGiftsMessageComposer : IMessageComposer
    {
        private Subscription subscription;
        private List<SubscriptionGift> gifts;

        public CatalogueClubGiftsMessageComposer(Subscription subscription, List<SubscriptionGift> gifts)
        {
            this.subscription = subscription;
            this.gifts = gifts;
        }

        public override void Write()
        {
            if (subscription != null)
            {
                m_Data.Add((int)(subscription.Data.GiftDate - DateTime.Now).TotalDays);
                m_Data.Add(subscription.Data.GiftsRedeemable);
            }
            else
            {
                m_Data.Add(0);
                m_Data.Add(0);
            }

            m_Data.Add(gifts.Count);

            foreach (var giftData in gifts)
            {
                PurchaseOKComposer.SerialiseOffer(this, giftData.CatalogueItem);
            }

            m_Data.Add(gifts.Count);

            foreach (var giftData in gifts)
            {
                m_Data.Add(giftData.CatalogueItem.Data.Id);
                m_Data.Add(false); // ?? unused

                if (subscription != null)
                {
                    var secondsForGift = giftData.GetSecondsRequired();

                    m_Data.Add(giftData.GetDaysUntilGift(subscription.Data.SubscriptionAge)); // days until gift allowed with 0 or less being yes you can select
                    m_Data.Add(giftData.IsGiftRedeemable(subscription.Data.SubscriptionAge)); // button to enable or not 
                }
                else
                {
                    m_Data.Add(0); // days until gift allowed with 0 or less being yes you can select
                    m_Data.Add(false); // button to enable or not 
                }
            }
        }
    }
}
