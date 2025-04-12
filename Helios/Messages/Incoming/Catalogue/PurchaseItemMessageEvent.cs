using System.Linq;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Util;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    class PurchaseItemMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int pageId = request.ReadInt();
            var cataloguePage = CatalogueManager.Instance.GetPage(pageId, avatar.Details.Rank, avatar.IsSubscribed);

            if (cataloguePage == null)
                return;

            int itemId = request.ReadInt();
            var catalogueItem = cataloguePage.Items.Where(x => x.Data.Id == itemId).FirstOrDefault();

            if (catalogueItem == null)
            {
                if (SubscriptionManager.Instance.IsSubscriptionItem(pageId, itemId))
                    SubscriptionManager.Instance.PurchaseClub(avatar, pageId, itemId);

                return;
            }

            //if (catalogueItem.Definition != null && catalogueItem.Definition.HasBehaviour(ItemBehaviour.EFFECT))
            //    return; // Effects disabled for now

            string extraData = request.ReadString().FilterInput(false);

            // Calculate new price for both credits and seasonal furniture
            int priceCoins = catalogueItem.Data.PriceCoins;
            int priceSeasonal = catalogueItem.Data.PriceSeasonal;

            // Continue standard purchase
            if (priceCoins > avatar.Details.Credits)
            {
                avatar.Send(new NoCreditsComposer(true, false));
                return;
            }

            if (catalogueItem.Data.SeasonalType is Storage.Models.Catalogue.SeasonalCurrencyType currency &&
                priceSeasonal > avatar.Currency.GetBalance(currency))
            {
                avatar.Send(new NoCreditsComposer(false, true, currency));
                return;
            }

            // Update credits of user
            if (priceCoins > 0)
            {
                avatar.Currency.ModifyCredits(-priceCoins);
                avatar.Currency.UpdateCredits();
            }

            // Update seasonal currency
            if (catalogueItem.Data.SeasonalType is Storage.Models.Catalogue.SeasonalCurrencyType currency2 &&
                priceSeasonal > 0)
            {
                avatar.Currency.AddBalance(currency2, -priceSeasonal);
                avatar.Currency.UpdateCurrency(currency2, false);
                avatar.Currency.SaveCurrencies();
            }

            CatalogueManager.Instance.Purchase(avatar.Details.Id, catalogueItem.Data.Id, 1, extraData, DateUtil.GetUnixTimestamp());
        }

        public int HeaderId => 100;
    }
}
