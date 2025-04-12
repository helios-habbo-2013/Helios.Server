using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class AvailableBadgesComposer : IMessageComposer
    {
        public override void Write()
        {
            this.AppendInt32(0);
            this.AppendInt32(0);
        }

        public override int HeaderId => 229;
    }
}
