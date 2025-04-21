using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class PingMessageComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 50;
    }
}