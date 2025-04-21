using Helios.Game;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class BuddyRequestsComposer : IMessageComposer
    {
        private List<MessengerUser> requests;

        public BuddyRequestsComposer(List<MessengerUser> requests)
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