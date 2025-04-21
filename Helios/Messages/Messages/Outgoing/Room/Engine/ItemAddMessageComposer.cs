using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ItemAddMessageComposer : IMessageComposer
    {
        private Item item;

        public ItemAddMessageComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 83;
    }
}