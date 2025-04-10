using Helios.Messages.Outgoing;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Catalogue;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Game
{
    public class SubscriptionManager : ILoadable
    {
        #region Fields

        public static readonly SubscriptionManager Instance = new SubscriptionManager();

        #endregion

        #region Properties

        public List<CatalogueSubscriptionData> Subscriptions { get; private set; }
        public List<SubscriptionGift> Gifts { get; private set; }

        #endregion

        #region Constructors

        public void Load()
        {
            Log.ForContext<SubscriptionManager>().Information("Loading Habbo Club subscription data");

            using (var context = new StorageContext())
            {
                Subscriptions = context.GetSubscriptionData();

                Gifts = context.GetSubscriptionGifts()
                    .Select(x => new SubscriptionGift(x, CatalogueManager.Instance.GetItem(x.SaleCode)))
                    .OrderBy(x => x.Data.DurationRequirement)
                    .ToList();
            }

            Log.ForContext<SubscriptionManager>().Information("Loaded {Count} of Habbo Club subscriptions", Subscriptions.Count);
            Log.ForContext<SubscriptionManager>().Information("Loaded {Count} of Habbo Club gifts", Gifts.Count);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get if the requested item is actually a subscription
        /// </summary>
        public SubscriptionGift GetGift(string spriteName)
        {
            return Gifts.Where(x => x.CatalogueItem.Data.SaleCode == spriteName || (x.CatalogueItem.Definition != null && x.CatalogueItem.Definition.Data.Sprite == spriteName)).FirstOrDefault();
        }

        /// <summary>
        /// Get if the requested item is actually a subscription
        /// </summary>
        public bool IsSubscriptionItem(int pageId, int itemId)
        {
            var subscriptionData = Subscriptions.Where(x => x.Id == itemId && x.PageId == pageId).FirstOrDefault();

            if (subscriptionData == null)
                return false;

            return true;
        }

        /// <summary>
        /// Handler for purchasing club
        /// </summary>
        public void PurchaseClub(Avatar avatar, int pageId, int itemId)
        {
            var subscriptionData = Subscriptions.Where(x => x.Id == itemId && x.PageId == pageId).FirstOrDefault();

            if (subscriptionData == null)
                return;

            // Calculate new price for both credits and seasonal furniture
            int priceCoins = subscriptionData.PriceCoins;
            int priceSeasonal = subscriptionData.PriceSeasonal;

            // Continue standard purchase
            if (priceCoins > avatar.Details.Credits)
            {
                avatar.Send(new NoCreditsComposer(true, false));
                return;
            }

            if (subscriptionData.SeasonalType is SeasonalCurrencyType currencyType)
            {
                if (priceSeasonal > avatar.Currency.GetBalance(currencyType))
                {
                    avatar.Send(new NoCreditsComposer(false, true, currencyType));
                    return;
                }

                if (priceSeasonal > 0)
                {
                    avatar.Currency.AddBalance(currencyType, -priceSeasonal);
                    avatar.Currency.UpdateCurrency(currencyType, false);
                    avatar.Currency.SaveCurrencies();
                }
            }

            // Update credits if needed
            if (priceCoins > 0)
            {
                avatar.Currency.ModifyCredits(-priceCoins);
                avatar.Currency.UpdateCredits();
            }


            avatar.Subscription.AddMonths(subscriptionData.Months);
        }

        #endregion
    }
}
