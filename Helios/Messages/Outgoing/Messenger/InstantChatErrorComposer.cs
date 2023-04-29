namespace Helios.Messages.Outgoing
{
    public class InstantChatErrorComposer : IMessageComposer
    {
        private InstantChatError instantChatError;
        private int AvatarId;
        private string message = string.Empty;

        public InstantChatErrorComposer(InstantChatError instantChatError, int AvatarId)
        {
            this.instantChatError = instantChatError;
            this.AvatarId = AvatarId;
        }

        public override void Write()
        {
            m_Data.Add((int)instantChatError);
            m_Data.Add(AvatarId);
            m_Data.Add(message);
        }
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