using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class FurniListRemoveComposer : IMessageComposer
    {
        private int id;

        public FurniListRemoveComposer(int id)
        {
            this.id = id;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 99;
    }
}