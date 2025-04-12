using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class UpdateWallItemComposer : IMessageComposer
    {
        private Item item;

        public UpdateWallItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            WallItemsComposer.Serialize(this, item);

            _data.Add(item.Data.OwnerData.Id);
            _data.Add(item.Data.OwnerData.Name);
        }

        public int HeaderId => -1;
    }
}
