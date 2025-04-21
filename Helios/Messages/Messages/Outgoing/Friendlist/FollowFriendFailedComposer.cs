using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class FollowFriendFailedComposer : IMessageComposer
    {
        private FollowBuddyError followBuddyError;

        public FollowFriendFailedComposer(FollowBuddyError followBuddyError)
        {
            this.followBuddyError = followBuddyError;
        }

        public override void Write()
        {
            _data.Add((int)followBuddyError);
        }

        public override int HeaderId => 349;
    }

    public enum FollowBuddyError
    {
        NotFriend = 0,
        Offline = 1,
        HotelView = 2,
        Prevented = 3
    }
}