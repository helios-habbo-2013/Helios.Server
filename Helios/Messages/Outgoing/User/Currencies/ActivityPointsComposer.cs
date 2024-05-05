using Helios.Storage.Models.Catalogue;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class ActivityPointsComposer : IMessageComposer
    {
        private Dictionary<SeasonalCurrencyType, int> currencies;

        public ActivityPointsComposer(Dictionary<SeasonalCurrencyType, int> currencies)
        {
            this.currencies = currencies;
        }

        public override void Write()
        {
            _data.Add(currencies.Count);

            foreach (var currency in currencies)
            {
                _data.Add((int)currency.Key);
                _data.Add(currency.Value);
            }
        }
    }
}
