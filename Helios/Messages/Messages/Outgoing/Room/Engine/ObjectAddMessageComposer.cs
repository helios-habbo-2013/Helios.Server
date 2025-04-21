using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ObjectAddMessageComposer : IMessageComposer
    {
        private Item item;

        public ObjectAddMessageComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 93;
    }
}