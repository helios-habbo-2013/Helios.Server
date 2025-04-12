namespace Helios.Messages.Outgoing
{
    class RoomGroupBadgeComposer : IMessageComposer
    {
        public override void Write()
        {
            this.AppendInt32(0);
        }

        public override int HeaderId => 309;
    }
}
