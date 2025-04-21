using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class GenericErrorComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 33;
    }
}