using Helios.Game;

namespace Helios.Messages.Outgoing
{
    internal class CataloguePageComposer : IMessageComposer
    {
        private CataloguePage page;

        public CataloguePageComposer(CataloguePage page)
        {
            this.page = page;
        }

        public override void Write()
        {
            _data.Add(page.Data.Id);
            _data.Add(page.Data.Layout);

            _data.Add(page.Images.Count);

            foreach (var image in page.Images)
                _data.Add(image);

            _data.Add(page.Texts.Count);

            foreach (var text in page.Texts)
                _data.Add(text);

            _data.Add(page.Items.Count);

            foreach (CatalogueItem item in page.Items)
            {
                PurchaseOKComposer.SerialiseOffer(this, item);
            }

            _data.Add(-1);
            _data.Add(false);

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
    }
}
 