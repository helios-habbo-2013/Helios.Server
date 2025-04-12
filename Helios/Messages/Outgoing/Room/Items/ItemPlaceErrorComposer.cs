namespace Helios.Messages.Outgoing
{
    class ItemPlaceErrorComposer : IMessageComposer
    {
        private ItemPlaceError itemPlaceError;

        public ItemPlaceErrorComposer(ItemPlaceError itemPlaceError)
        {
            this.itemPlaceError = itemPlaceError;
        }

        public override void Write()
        {
            _data.Add((int)itemPlaceError);
        }

        public int HeaderId => -1;
    }
    
    public enum ItemPlaceError
    {
        NoRights = 1,
        NoPlacementAllowed = 11
    }
}
