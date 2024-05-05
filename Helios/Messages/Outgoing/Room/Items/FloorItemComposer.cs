using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class FloorItemComposer : IMessageComposer
    {
        private Item floorItem;

        public FloorItemComposer(Item floorItem)
        {
            this.floorItem = floorItem;
        }

        public override void Write()
        {
            FloorItemsComposer.Serialize(this, floorItem);

            m_Data.Add(floorItem.Data.OwnerData.Id);
            m_Data.Add(floorItem.Data.OwnerData.Name);
        }
    }
}
