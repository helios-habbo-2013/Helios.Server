using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class YouAreControllerMessageComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 42;
    }
}