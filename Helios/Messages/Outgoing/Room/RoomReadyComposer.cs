namespace Helios.Messages.Outgoing
{
    public class RoomReadyComposer : IMessageComposer
    {
        private string modelName;
        private int roomId;

        public RoomReadyComposer(string modelName, int roomId)
        {
            this.modelName = modelName;
            this.roomId = roomId;
        }

        public override void Write()
        {
            _data.Add(modelName);
            _data.Add(roomId);
        }

        public int HeaderId => -1;
    }
}
