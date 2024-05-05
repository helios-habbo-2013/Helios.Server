namespace Helios.Messages.Outgoing
{
    class RoomForwardComposer : IMessageComposer
    {
        private int roomId;
        private bool isPublicRoom;

        public RoomForwardComposer(int roomId, bool isPublicRoom)
        {
            this.roomId = roomId;
            this.isPublicRoom = isPublicRoom;
        }

        public override void Write()
        {
            _data.Add(isPublicRoom);
            _data.Add(roomId);
        }
    }
}
