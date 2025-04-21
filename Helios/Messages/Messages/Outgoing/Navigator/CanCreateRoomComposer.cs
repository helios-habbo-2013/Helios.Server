using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class CanCreateRoomComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 0x0200;
    }
}