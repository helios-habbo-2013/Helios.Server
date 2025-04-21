using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class LatencyPingResponseMessageComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 354;
    }
}