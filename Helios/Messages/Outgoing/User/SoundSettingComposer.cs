using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class SoundSettingComposer : IMessageComposer
    {
        public override void Write()
        {
            this.AppendBoolean(true);
            this.AppendInt32(0);
        }

        public override int HeaderId => 308;
    }
}
