using Helios.Messages.Outgoing;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Avatar;
using Helios.Storage.Models.Catalogue;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Game
{
    public class CurrencyManager : ILoadable
    {
        #region Properties

        private Avatar avatar;
        public Dictionary<SeasonalCurrencyType, int> Currencies;

        #endregion

        #region Constructor

        public CurrencyManager(Avatar avatar)
        {
            this.avatar = avatar;
        }

        public void Load()
        {
            using (var context = new StorageContext())
            {
                this.Currencies = context.GetCurrencies(avatar.Details.Id).ToDictionary(x => x.SeasonalType, x => x.Balance < 0 ? 0 : x.Balance);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get the balance for this seasonal currency
        /// </summary>
        public int GetBalance(SeasonalCurrencyType currencyType)
        {
            return Currencies.TryGetValue(currencyType, out var balance) ? balance : 0;
        }

        /// <summary>
        /// Set the balance for this seasonal currency
        /// </summary>
        public void SetBalance(SeasonalCurrencyType currencyType, int newBalance)
        {
            Currencies[currencyType] = newBalance;
        }

        /// <summary>
        /// Add the balance for this seasonal currency (will also accept negatives)
        /// </summary>
        public void AddBalance(SeasonalCurrencyType currencyType, int newBalance)
        {
            using (var context = new StorageContext())
            {
                Currencies[currencyType] = context.GetCurrency(avatar.Details.Id, currencyType).Balance + newBalance;
            }
        }

        /// <summary>
        /// Refresh user credits from db but also override them
        /// </summary>
        public void ModifyCredits(int creditsChanged)
        {
            using (var context = new StorageContext())
            {
                avatar.Details.Credits = context.SaveCredits(avatar.Details.Id, creditsChanged);
            }
        }

        /// <summary>
        /// Update curencies on client side
        /// </summary>
        public void UpdateCredits()
        {
            avatar.Send(new CreditBalanceComposer(avatar.Details.Credits));
        }

        /// <summary>
        /// Update curencies on client side
        /// </summary>
        public void UpdateCurrencies()
        {
            avatar.Send(new HabboActivityPointNotificationMessageComposer(this.GetBalance(SeasonalCurrencyType.DEFAULT), HabboActivityPointNotificationMessageComposer.ActivityPointAlertType.NO_SOUND));
            //avatar.Send(new ActivityPointsComposer(Currencies));
        }

        /// <summary>
        /// Updates singular currency on client side
        /// </summary>
        public void UpdateCurrency(SeasonalCurrencyType seasonalCurrency, bool notify = true)
        {
            // if (Currencies.TryGetValue(seasonalCurrency, out var balance))
            //    avatar.Send(new ActivityPointsNotificationComposer(seasonalCurrency, balance, notify));
        }

        /// <summary>
        /// Save list of currencies for user
        /// </summary>
        public void SaveCurrencies()
        {
            List<CurrencyData> currencyList = Currencies
                .Select(kvp => new CurrencyData { AvatarId = avatar.Details.Id, SeasonalType = kvp.Key, Balance = kvp.Value })
                .ToList();

            using (var context = new StorageContext())
            {
                context.SaveCurrencies(currencyList);
            }
        }

        #endregion
    }
}
