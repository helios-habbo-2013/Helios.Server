namespace Helios.Storage.Database.Data
{
    public class CurrencyData
    {
        public virtual int AvatarId { get; set; }
        public virtual SeasonalCurrencyType SeasonalType { get; set; }
        public virtual int Balance { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as CurrencyData;

            if (t == null)
                return false;

            if (AvatarId == t.AvatarId && SeasonalType == t.SeasonalType)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
