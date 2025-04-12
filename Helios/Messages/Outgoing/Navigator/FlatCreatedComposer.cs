namespace Helios.Messages.Outgoing
{
    class FlatCreatedComposer : IMessageComposer
    {
        private int roomId;
        private string roomName;

        public FlatCreatedComposer(int roomId, string roomName)
        {
            this.roomId = roomId;
            this.roomName = roomName;
        }

        public override void Write()
        {
            _data.Add(roomId);
            _data.Add(roomName);
        }

        public int HeaderId => -1;
    }
}
