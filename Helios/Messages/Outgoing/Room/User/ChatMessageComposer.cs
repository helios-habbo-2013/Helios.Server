namespace Helios.Messages.Outgoing
{
    public class ChatMessageComposer : IMessageComposer
    {
        private int instanceId;
        private string message;
        private int colour;
        private int gesture;

        public ChatMessageComposer(int instanceId, string message, int colour, int gesture)
        {
            this.instanceId = instanceId;
            this.message = message;
            this.colour = colour;
            this.gesture = gesture;
        }

        public override void Write()
        {
            _data.Add(instanceId);
            _data.Add(message);
            _data.Add(gesture);
            _data.Add(colour);
            _data.Add(0);
            _data.Add(-1);
        }

        public int HeaderId => -1;
    }
}
