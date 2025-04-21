using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ClubGiftSelectedComposer : IMessageComposer
    {
        private CatalogueItem _item;

        public ClubGiftSelectedComposer(CatalogueItem offer)
        {
            this._item = offer;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(_item.Data.SaleCode);
            this.AppendInt32(_item.Packages.Count);

            foreach (CataloguePackage package in _item.Packages)
            {
                this.AppendStringWithBreak(package.Definition.Type);
                this.AppendInt32(package.Definition.Data.SpriteId);
                this.AppendStringWithBreak(package.Data.SpecialSpriteId);
                this.AppendInt32(package.Data.Amount);
                this.AppendBoolean(false);
            }
        }

        public override int HeaderId => 624;
    }
}