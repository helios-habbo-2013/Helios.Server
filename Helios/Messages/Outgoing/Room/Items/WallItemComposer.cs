using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class WallItemComposer : IMessageComposer
    {
        private Item item;

        public WallItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            WallItemsComposer.Serialize(this, item);

            m_Data.Add(item.Data.OwnerData.Id);
            m_Data.Add(item.Data.OwnerData.Name);
        }
    }
}
