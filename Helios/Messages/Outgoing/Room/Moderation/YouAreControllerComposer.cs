namespace Helios.Messages.Outgoing
{
    class YouAreControllerComposer : IMessageComposer
    {

        public override void Write() { }

        public override int HeaderId => 42;
    }
}
