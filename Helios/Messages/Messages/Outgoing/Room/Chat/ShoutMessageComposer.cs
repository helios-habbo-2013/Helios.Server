using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ShoutMessageComposer : IMessageComposer
    {
        private int instanceId;
        private string chatMsg;
        private int colourId;
        private int v;

        public ShoutMessageComposer(int instanceId, string chatMsg, int colourId, int v)
        {
            this.instanceId = instanceId;
            this.chatMsg = chatMsg;
            this.colourId = colourId;
            this.v = v;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 26;
    }
}