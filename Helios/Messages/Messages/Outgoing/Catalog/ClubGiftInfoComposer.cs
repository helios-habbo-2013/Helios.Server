using Helios.Game;
using System.Collections.Generic;
using System;

namespace Helios.Messages.Outgoing
{
    class ClubGiftInfoComposer : IMessageComposer
    {
        private Subscription subscription;
        private List<SubscriptionGift> gifts;

        public ClubGiftInfoComposer(Subscription subscription, List<SubscriptionGift> gifts)
        {
            this.subscription = subscription;
            this.gifts = gifts;
        }

        public override void Write()
        {
            if (subscription != null)
            {
                this.AppendInt32((int)(subscription.Data.GiftDate - DateTime.Now).TotalDays);
                this.AppendInt32(subscription.Data.GiftsRedeemable);
            }
            else
            {
                this.AppendInt32(0);
                this.AppendInt32(0);
            }

            this.AppendInt32(gifts.Count);

            foreach (var giftData in gifts)
            {
                PurchaseOKMessageComposer.SerialiseOffer(this, giftData.CatalogueItem, giftData.Data.DurationRequirement);
            }

            this.AppendInt32(gifts.Count);

            foreach (var giftData in gifts)
            {
                this.AppendInt32(giftData.CatalogueItem.Data.Id);
                this.AppendBoolean(false); // ?? unused
                this.AppendInt32(giftData.Data.DurationRequirement); // days until gift allowed with 0 or less being yes you can select

                if (subscription != null)
                {
                    var secondsForGift = giftData.GetSecondsRequired();
                    this.AppendBoolean(giftData.IsGiftRedeemable(subscription.Data.SubscriptionAge)); // button to enable or not 
                }
                else
                {
                    // _data.Add(0); // days until gift allowed with 0 or less being yes you can select
                    this.AppendBoolean(false); // button to enable or not 
                }
            }
        }


        public override int HeaderId => 623;
    }
}