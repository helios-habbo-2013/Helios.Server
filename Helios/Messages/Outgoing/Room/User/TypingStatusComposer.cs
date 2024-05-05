namespace Helios.Messages.Outgoing
{
    public class TypingStatusComposer : IMessageComposer
    {
        private int instanceId;
        private bool isTyping;

        public TypingStatusComposer(int instanceId, bool isTyping)
        {
            this.instanceId = instanceId;
            this.isTyping = isTyping;
        }

        public override void Write()
        {
            _data.Add(instanceId);
            _data.Add(isTyping ? 1 : 0);
        }
    }
}
