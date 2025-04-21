using Helios.Game;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class UserRightsMessageComposer : IMessageComposer
    {
        private List<string> fuserights;

        public UserRightsMessageComposer(List<string> list)
        {
            this.fuserights = list;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 2;
    }
}