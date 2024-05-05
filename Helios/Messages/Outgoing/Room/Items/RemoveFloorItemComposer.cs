using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class RemoveFloorItemComposer : IMessageComposer
    {
        private Item item;

        public RemoveFloorItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            m_Data.Add(item.Id.ToString());
            m_Data.Add(0);
            m_Data.Add(item.Data.OwnerId);
        }
    }
}
