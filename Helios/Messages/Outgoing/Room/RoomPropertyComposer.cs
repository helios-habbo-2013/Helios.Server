namespace Helios.Messages.Outgoing
{
    class RoomPropertyComposer : IMessageComposer
    {
        private string key;
        private string value;

        public RoomPropertyComposer(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(key);
            this.AppendStringWithBreak(value);
        }

        public override int HeaderId => 46;
    }
}
