using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class RoomForwardMessageComposer : IMessageComposer
    {
        private int roomId;
        private bool isPublicRoom;

        public RoomForwardMessageComposer(int id, bool isPublicRoom)
        {
            this.roomId = id;
            this.isPublicRoom = isPublicRoom;
        }

        public override void Write()
        {
            _data.Add(isPublicRoom);
            _data.Add(roomId);
        }

        public override int HeaderId => 286;
    }
}