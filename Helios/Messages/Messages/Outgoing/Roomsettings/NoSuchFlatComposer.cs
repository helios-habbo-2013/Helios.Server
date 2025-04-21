using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class NoSuchFlatComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 44;
    }
}