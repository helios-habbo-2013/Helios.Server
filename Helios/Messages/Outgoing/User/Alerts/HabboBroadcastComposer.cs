namespace Helios.Messages.Outgoing
{
    class HabboBroadcastComposer : IMessageComposer
    {
        private readonly string message;
        public HabboBroadcastComposer(string message)
        {
            this.message = message;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(this.message);
        }

        public override int HeaderId => 139;
    }
}
