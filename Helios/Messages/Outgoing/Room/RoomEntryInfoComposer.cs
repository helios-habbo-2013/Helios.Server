using Helios.Storage.Models.Room;

namespace Helios.Messages.Outgoing
{
    class RoomEntryInfoComposer : IMessageComposer
    {
        private RoomData roomData;
        private bool isOwner;

        public RoomEntryInfoComposer(RoomData roomData, bool isOwner)
        {
            this.roomData = roomData;
            this.isOwner = isOwner;
        }

        public override void Write()
        {
            _data.Add(roomData.IsPrivateRoom);

            if (roomData.IsPrivateRoom)
            {
                _data.Add(roomData.Id);
                _data.Add(isOwner);
            }
            else
            {
                _data.Add(roomData.Description);
                _data.Add(roomData.Id);
            }
        }

        public override int HeaderId => 471;
    }
}
