namespace Helios.Storage.Database.Data
{
    public class MessengerUserData
    {
        public virtual int AvatarId { get; set; }
        public virtual int FriendId { get; set; }
        public virtual AvatarData FriendData { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as MessengerUserData;

            if (t == null)
                return false;

            if (FriendId == t.FriendId &&
                AvatarId == t.AvatarId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
