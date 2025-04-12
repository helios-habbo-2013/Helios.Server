using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class UpdateFloorItemComposer : IMessageComposer
    {
        private Item item;

        public UpdateFloorItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            FloorItemsComposer.Serialize(this, item);

            _data.Add(item.Data.OwnerData.Id);
            _data.Add(item.Data.OwnerData.Name);
        }

        public int HeaderId => -1;
    }
}
