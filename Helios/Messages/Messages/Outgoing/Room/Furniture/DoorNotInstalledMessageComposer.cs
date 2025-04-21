using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class DoorNotInstalledMessageComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 64;
    }
}