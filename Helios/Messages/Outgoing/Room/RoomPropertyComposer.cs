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
            _data.Add(key);
            _data.Add(value);
        }
    }
}
