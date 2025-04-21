using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class NewConsoleMessageComposer : IMessageComposer
    {
        private int fromId;
        private string message;

        public NewConsoleMessageComposer(int fromId, string message)
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