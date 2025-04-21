using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class UserTypingMessageComposer : IMessageComposer
    {
        private int instanceId;
        private bool v;

        public UserTypingMessageComposer(int instanceId, bool v)
        {
            this.instanceId = instanceId;
            this.v = v;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 361;
    }
}