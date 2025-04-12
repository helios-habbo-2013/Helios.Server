using Helios.Storage.Models.Catalogue;

namespace Helios.Messages.Outgoing
{
    public class NoCreditsComposer : IMessageComposer
    {
        private bool hasEnoughCredits;
        private bool hasEnoughSeasonalCurrency;
        private SeasonalCurrencyType? seasonalCurrencyType;

        public NoCreditsComposer(bool hasEnoughCredits, bool hasEnoughSeasonalCurrency)
        {
            this.hasEnoughCredits = hasEnoughCredits;
            this.hasEnoughSeasonalCurrency = hasEnoughSeasonalCurrency;
        }

        public NoCreditsComposer(bool hasEnoughCredits, bool hasEnoughSeasonalCurrency, SeasonalCurrencyType seasonalCurrencyType)
        {
            this.hasEnoughCredits = hasEnoughCredits;
            this.hasEnoughSeasonalCurrency = hasEnoughSeasonalCurrency;
            this.seasonalCurrencyType = seasonalCurrencyType;
        }

        public override void Write()
        {
            _data.Add(hasEnoughCredits);
            _data.Add(hasEnoughSeasonalCurrency);

            if (seasonalCurrencyType != null)
                _data.Add((int)seasonalCurrencyType.Value);
        }

        public override int HeaderId => 68;
    }
}
