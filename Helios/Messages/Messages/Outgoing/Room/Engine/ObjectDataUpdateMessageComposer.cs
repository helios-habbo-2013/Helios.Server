using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ObjectDataUpdateMessageComposer : IMessageComposer
    {
        private Item _item;

        public ObjectDataUpdateMessageComposer(Item item)
        {
            this._item = item;
        }

        public override void Write()
        {
            ObjectsMessageComposer.Serialize(this, _item);
        }

        public override int HeaderId => 88;
    }
}