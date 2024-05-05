using Helios.Storage.Models.Subscription;

namespace Helios.Messages.Outgoing
{
    internal class ScrSendUserInfoComposer : IMessageComposer
    {
        private SubscriptionData subscription;

        public ScrSendUserInfoComposer(SubscriptionData subscription)
        {
            this.subscription = subscription;
        }

        public override void Write()
        {
            _data.Add("habbo_club"); // Which product/widget to assign the value
            _data.Add(subscription != null ? subscription.DaysLeft : 0); // DAYS LEFT
            _data.Add(0); // unused ??
            _data.Add(subscription != null ? subscription.MonthsLeft : 0); // MONTHS LEFT
            _data.Add(0); // unused ??
            _data.Add(false); // unused ??
            _data.Add(subscription != null);
            _data.Add(0); // unused ??
            _data.Add(0); // unused ??
            _data.Add(0); // unused ??
        }
    }
}