namespace Helios.Messages.Outgoing
{
    public class FurniListRemoveComposer : IMessageComposer
    {
        private int itemId;

        public FurniListRemoveComposer(int itemId)
        {
            this.itemId = itemId;
        }

        public override void Write()
        {
            _data.Add(itemId);
        }

        public override int HeaderId => -1;
    }
}
