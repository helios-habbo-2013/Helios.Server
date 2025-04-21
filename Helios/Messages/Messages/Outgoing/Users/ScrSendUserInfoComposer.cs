using Helios.Game;
using Helios.Storage.Models.Subscription;

namespace Helios.Messages.Outgoing
{
    class ScrSendUserInfoComposer : IMessageComposer
    {
        private SubscriptionData subscription;

        public ScrSendUserInfoComposer(SubscriptionData subscription)
        {
            this.subscription = subscription;
        }

        public override void Write()
        {
            _data.Add("habbo_club");
            _data.Add(subscription != null ? subscription.DaysLeft : 0);
            _data.Add(false);
            _data.Add(subscription != null ? subscription.MonthsLeft : 0);
        }

        public override int HeaderId => 7;

        /*
        _data.Add("habbo_club"); // Which product/widget to assign the value
        _data.Add(subscription != null ? subscription.DaysLeft : 0); // DAYS LEFT
        _data.Add(2); // unused ??
        _data.Add(subscription != null ? subscription.MonthsLeft : 0); // MONTHS LEFT
        _data.Add(1); // unused ??
        _data.Add(subscription != null); // unused ??
        _data.Add(true);
        _data.Add(0); // unused ??
        _data.Add(subscription != null ? subscription.DaysLeft : 0); // unused ??
        _data.Add(subscription != null ? 1 : 0); // group membership purchase enabled / disabled
        */
    }
}