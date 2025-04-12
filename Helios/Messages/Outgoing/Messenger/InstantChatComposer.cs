namespace Helios.Messages.Outgoing
{
    public class InstantChatComposer : IMessageComposer
    {
        private int fromId;
        private string message;

        public InstantChatComposer(int fromId, string message)
        {
            this.fromId = fromId;
            this.message = message;
        }
        public override void Write()
        {
            _data.Add(fromId);
            _data.Add(message);
            _data.Add(0);
        }

        public override int HeaderId => 134;
    }
}