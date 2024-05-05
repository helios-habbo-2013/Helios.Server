using Helios.Storage.Models.Avatar;

namespace Helios.Messages.Outgoing.Messenger
{
    class MessengerRequestComposer : IMessageComposer
    {
        private AvatarData m_AvatarData;

        public MessengerRequestComposer(AvatarData avatarData)
        {
            m_AvatarData = avatarData;
        }

        public override void Write()
        {
            m_Data.Add(m_AvatarData.Id);
            m_Data.Add(m_AvatarData.Name);
            m_Data.Add(m_AvatarData.Figure);
        }
    }
}
