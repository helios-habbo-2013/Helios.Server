using System;
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
                _data.Add((int)(subscription.Data.GiftDate - DateTime.Now).TotalDays);
                _data.Add(subscription.Data.GiftsRedeemable);
            }
            else
            {
                _data.Add(0);
                _data.Add(0);
            }

            _data.Add(gifts.Count);

            foreach (var giftData in gifts)
            {
                PurchaseOKComposer.SerialiseOffer(this, giftData.CatalogueItem);
            }

            _data.Add(gifts.Count);

            foreach (var giftData in gifts)
            {
                _data.Add(giftData.CatalogueItem.Data.Id);
                _data.Add(false); // ?? unused

                if (subscription != null)
                {
                    var secondsForGift = giftData.GetSecondsRequired();

                    _data.Add(giftData.GetDaysUntilGift(subscription.Data.SubscriptionAge)); // days until gift allowed with 0 or less being yes you can select
                    _data.Add(giftData.IsGiftRedeemable(subscription.Data.SubscriptionAge)); // button to enable or not 
                }
                else
                {
                    _data.Add(0); // days until gift allowed with 0 or less being yes you can select
                    _data.Add(false); // button to enable or not 
                }
            }
        }
    }
}
