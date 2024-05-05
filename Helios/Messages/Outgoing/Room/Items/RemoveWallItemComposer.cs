using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class RemoveWallItemComposer : IMessageComposer
    {
        private Item item;

        public RemoveWallItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            _data.Add(item.Id.ToString());
            _data.Add(item.Data.OwnerId);
        }
    }
}
