namespace Helios.Messages.Outgoing
{
    public class CanCreateRoomComposer : IMessageComposer
    {
        private bool hasReachedLimit;
        private int maxRooms;

        public CanCreateRoomComposer(bool hasReachedLimit, int maxRooms)
        {
            this.hasReachedLimit = hasReachedLimit;
            this.maxRooms = maxRooms;
        }

        public override void Write()
        {
            _data.Add(hasReachedLimit ? 0 : 1);
            _data.Add(maxRooms);
        }

        public override int HeaderId => -1;
    }
}