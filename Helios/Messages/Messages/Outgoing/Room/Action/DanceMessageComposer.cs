using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class DanceMessageComposer : IMessageComposer
    {
        private int instanceId;
        private int v;

        public DanceMessageComposer(int instanceId, int v)
        {
            this.instanceId = instanceId;
            this.v = v;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 480;
    }
}