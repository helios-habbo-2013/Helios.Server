namespace Helios.Storage.Database.Data
{
    public class CatalogueItemData
    {
        public virtual int Id { get; set; }
        public virtual string SaleCode { get; set; }
        public virtual string PageId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int PriceCoins { get; set; }
        public virtual int PriceSeasonal { get; set; }
        public virtual SeasonalCurrencyType SeasonalType { get; set; }
        public virtual bool IsHidden { get; set; }
        public virtual int Amount { get; set; }
        public virtual int DefinitionId { get; set; }
        public virtual string SpecialSpriteId { get; set; }
        public virtual bool IsPackage { get; set; }
        public virtual bool AllowBulkPurchase { get; set; }
    }

    public enum SeasonalCurrencyType
    {
        PUMPKINS = 101,
        PEANUTS = 102,
        STARS = 103,
        CLOUDS = 104,
        DIAMONDS = 105,
        LOYALTY_POINTS = 3,
        DUCKETS = 0
    }
}
