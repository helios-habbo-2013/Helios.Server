using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class InstantMessageErrorComposer : IMessageComposer
    {
        private InstantChatError instantChatError;
        private int AvatarId;
        private string message = string.Empty;

        public InstantMessageErrorComposer(InstantChatError instantChatError, int AvatarId)
        {
            this.instantChatError = instantChatError;
            this.AvatarId = AvatarId;
        }

        public override void Write()
        {
            _data.Add((int)instantChatError);
            _data.Add(AvatarId);
            _data.Add(message);
        }

        public override int HeaderId => 261;
    }

    public enum InstantChatError
    {
        ReceiverMuted = 3,
        SenderMuted = 4,
        FriendOffline = 5,
        NotFriend = 6,
        FriendBusy = 7,
        SendingFailed = 10,
    }
}