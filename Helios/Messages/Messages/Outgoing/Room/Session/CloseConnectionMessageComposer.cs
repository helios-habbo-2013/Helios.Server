using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class CloseConnectionMessageComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 18;
    }
}