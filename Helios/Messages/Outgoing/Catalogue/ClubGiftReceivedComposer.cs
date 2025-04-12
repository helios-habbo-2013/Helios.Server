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
            this.AppendStringWithBreak(item.Data.SaleCode);
            this.AppendInt32(item.Packages.Count);

            foreach (CataloguePackage package in item.Packages)
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
