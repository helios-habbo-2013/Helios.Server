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
            _data.Add(m_AvatarData.Id);
            _data.Add(m_AvatarData.Name);
            _data.Add(m_AvatarData.Figure);
        }
    }
}
