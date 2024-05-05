using Helios.Storage.Models.Catalogue;

namespace Helios.Messages.Outgoing
{
    class ActivityPointsNotificationComposer : IMessageComposer
    {
        private SeasonalCurrencyType currencyType;
        private int balance;
        private bool notifyClient;

        public ActivityPointsNotificationComposer(SeasonalCurrencyType currencyType, int balance, bool notifyClient)
        {
            this.currencyType = currencyType;
            this.balance = balance;
            this.notifyClient = notifyClient;
        }

        public override void Write()
        {        
            _data.Add(balance);
            _data.Add(notifyClient ? 1 : 0);
            _data.Add((int)currencyType);
        }
    }
}
