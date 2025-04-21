using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ChatMessageComposer : IMessageComposer
    {
        private int instanceId;
        private string chatMsg;
        private int colourId;
        private int v;

        public ChatMessageComposer(int instanceId, string chatMsg, int colourId, int v)
        {
            this.instanceId = instanceId;
            this.chatMsg = chatMsg;
            this.colourId = colourId;
            this.v = v;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 24;
    }
}