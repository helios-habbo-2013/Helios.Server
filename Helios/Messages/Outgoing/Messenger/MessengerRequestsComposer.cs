using System.Collections.Generic;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class MessengerRequestsComposer : IMessageComposer
    {
        private List<MessengerUser> requests;

        public MessengerRequestsComposer(List<MessengerUser> requests)
        {
            this.requests = requests;
        }

        public override void Write()
        {
            m_Data.Add(requests.Count);
            m_Data.Add(requests.Count);

            foreach (var request in requests)
            {
                m_Data.Add(request.AvatarData.Id);
                m_Data.Add(request.AvatarData.Name);
                m_Data.Add(request.AvatarData.Figure);
            }
        }
    }
}