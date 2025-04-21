using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class BadgesComposer : IMessageComposer
    {
        public override void Write()
        {
            this.AppendInt32(0);
            this.AppendInt32(0);
        }

        public override int HeaderId => 229;
    }
}