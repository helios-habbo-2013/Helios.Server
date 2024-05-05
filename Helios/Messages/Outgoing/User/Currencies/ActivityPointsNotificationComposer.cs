﻿namespace Helios.Messages.Outgoing
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
            m_Data.Add(balance);
            m_Data.Add(notifyClient ? 1 : 0);
            m_Data.Add((int)currencyType);
        }
    }
}
