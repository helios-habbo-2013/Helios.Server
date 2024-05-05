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
            _data.Add(item.Id.ToString());
            _data.Add(0);
            _data.Add(item.Data.OwnerId);
        }
    }
}
