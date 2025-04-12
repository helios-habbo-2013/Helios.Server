namespace Helios.Messages.Outgoing
{
    class RoomEventComposer : IMessageComposer
    {
        public override void Write()
        {
            this.AppendStringWithBreak("-1");
        }

        public override int HeaderId => 370;
    }
}
