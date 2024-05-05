using Helios.Storage.Models.Catalogue;

namespace Helios.Messages.Outgoing
{
    class CatalogueItemDiscountComposer : IMessageComposer
    {
        private CatalogueDiscountData discount;

        public CatalogueItemDiscountComposer(CatalogueDiscountData discount)
        {
            this.discount = discount;
        }

        public override void Write()
        {
            _data.Add(discount.PurchaseLimit); // The discount / bulk buy limit
            _data.Add((int)discount.DiscountBatchSize); // A - "Buy A get B free"
            _data.Add((int)discount.DiscountAmountPerBatch); // B
            _data.Add((int)discount.MinimumDiscountForBonus); // minimum for bonus
            _data.Add(0);//Count
            /*{
                m_Data.Add(40);
                m_Data.Add(99);
            }*/
        }
    }
}
