using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ItemUpdateMessageComposer : IMessageComposer
    {
        private Item item;

        public ItemUpdateMessageComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            ItemsMessageComposer.Serialize(this, item);
        }

        public override int HeaderId => 85;
    }
}