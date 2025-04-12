using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class ClubGiftReceivedComposer : IMessageComposer
    {
        private CatalogueItem item;

        public ClubGiftReceivedComposer(CatalogueItem offer)
        {
            this.item = offer;
        }

        public override void Write()
        {
            _data.Add(item.Data.SaleCode);
            _data.Add(item.Packages.Count);

            foreach (CataloguePackage package in item.Packages)
            {
                _data.Add(package.Definition.Type);
                _data.Add(package.Definition.Data.SpriteId);
                _data.Add(package.Data.SpecialSpriteId);
                _data.Add(package.Data.Amount);
                _data.Add(false);
            }
        }

        public override int HeaderId => -1;
    }
}
