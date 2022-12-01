using System;

namespace Helios.Storage.Database.Data
{
    public class EffectData
    {
        public virtual int UserId { get; set; }
        public virtual int EffectId { get; set; }
        public virtual DateTime? ExpiresAt { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool IsActivated { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as EffectData;

            if (t == null)
                return false;

            if (UserId == t.UserId && EffectId == t.EffectId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
