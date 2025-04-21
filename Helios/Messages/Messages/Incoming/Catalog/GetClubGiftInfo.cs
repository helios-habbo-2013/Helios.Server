using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetClubGiftInfo : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Subscription.Refresh();
            avatar.Subscription.CountMemberDays();

            avatar.Send(new ClubGiftInfoComposer(avatar.IsSubscribed ? avatar.Subscription : null, SubscriptionManager.Instance.Gifts));
        }

        public int HeaderId => 474;
    }
}