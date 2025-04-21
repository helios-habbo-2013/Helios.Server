using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class CatalogPageMessageComposer : IMessageComposer
    {
        private CataloguePage page;

        public CatalogPageMessageComposer(CataloguePage page)
        {
            this.page = page;
        }

        public override void Write()
        {
            this.AppendInt32(page.Data.Id);
            this.AppendStringWithBreak(page.Data.Layout);
            this.AppendInt32(page.Images.Count);

            foreach (var image in page.Images)
            {
                this.AppendStringWithBreak(image);
            }

            this.AppendInt32(page.Texts.Count);

            foreach (var text in page.Texts)
            {
                this.AppendStringWithBreak(text);
            }

            this.AppendInt32(page.Items.Count);

            foreach (CatalogueItem item in page.Items)
            {
                PurchaseOKMessageComposer.SerialiseOffer(this, item);
            }

            this.AppendInt32(-1);
            /*
             foreach (CatalogueItem item in page.Items)
             {
                 m_Data.Add(item.Data.Id);
                 m_Data.Add(item.Data.SaleCode);
                 m_Data.Add(item.Data.PriceCoins);
                 m_Data.Add(item.Data.PricePixels);
                 m_Data.Add(0);
                 m_Data.Add(false);
                 m_Data.Add(item.Packages.Count);

                 foreach (CataloguePackage package in item.Packages)
                 {
                     m_Data.Add(package.Definition.Type);
                     m_Data.Add(package.Definition.Data.SpriteId);
                     m_Data.Add(package.Data.SpecialSpriteId); // extra data
                     m_Data.Add(package.Data.Amount);
                     //m_Data.Add(0);
                 }

                 m_Data.Add(true);

                 m_Data.Add(1000);
                 m_Data.Add(1000 - 24);
                 m_Data.Add(0);

                 m_Data.Add(true);
             }

             m_Data.Add(-1); // club level
             m_Data.Add(false);
             */
        }

        public override int HeaderId => 127;
    }
}
