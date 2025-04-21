using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class RoomInfoUpdatedComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 456;
    }
}