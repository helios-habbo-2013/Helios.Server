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
            _data.Add(requests.Count);
            _data.Add(requests.Count);

            foreach (var request in requests)
            {
                _data.Add(request.AvatarData.Id);
                _data.Add(request.AvatarData.Name);
                _data.Add(request.AvatarData.Figure);
            }
        }

        public override int HeaderId => 314;
    }
}