namespace Helios.Messages.Outgoing
{
    public class RoomUrlComposer : IMessageComposer
    {
        private int roomId;

        public RoomUrlComposer(int roomId)
        {
            this.roomId = roomId;
        }

        public override void Write()
        {
            this.AppendStringWithBreak($"/client/private/{roomId}/id");
        }

        public override int HeaderId => 166;
    }
}
