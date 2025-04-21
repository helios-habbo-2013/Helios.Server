using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class PetRemovedFromInventoryComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 604;
    }
}