namespace Helios.Messages.Outgoing
{
    class YouAreOwnerMessageEvent : IMessageComposer
    {
        public override void Write() { }

        public override int HeaderId => 47;
    }
}
