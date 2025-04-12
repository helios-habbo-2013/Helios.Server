using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class PurchaseOKComposer : IMessageComposer
    {
        private CatalogueItem item;
        private readonly int durationRequirement;

        public PurchaseOKComposer(CatalogueItem offer, int durationRequirement = -1)
        {
            this.item = offer;
            this.durationRequirement = durationRequirement;
        }

        public override void Write()
        {
            SerialiseOffer(this, item);
        }

        internal static void SerialiseOffer(IMessageComposer composer, CatalogueItem item, int durationRequirement = -1)//, bool spriteAsSaleCode = false)
        {
            composer.AppendInt32(item.Data.Id);
            composer.AppendStringWithBreak(item.Data.SaleCode);//composer.Data.Add(spriteAsSaleCode ? ItemManager.Instance.GetDefinition(item.Packages[0].Data.DefinitionId).Data.Sprite : item.Data.SaleCode);
            composer.AppendInt32(item.Data.PriceCoins);
            composer.AppendInt32(item.Data.PriceSeasonal);
            composer.AppendInt32(item.Packages.Count);

            foreach (CataloguePackage package in item.Packages)
            {
                composer.AppendStringWithBreak(package.Definition.Type);
                composer.AppendInt32(package.Definition.Data.SpriteId);
                composer.AppendStringWithBreak(package.Data.SpecialSpriteId); // extra data
                composer.AppendInt32(package.Data.Amount);
                composer.AppendInt32(durationRequirement);
            }
        }

        public override int HeaderId => 67;
    }
}
