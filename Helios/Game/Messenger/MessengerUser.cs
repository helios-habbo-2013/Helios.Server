using Helios.Storage.Database.Data;

namespace Helios.Game
{
    public class MessengerUser
    {
        #region Properties

        public AvatarData AvatarData
        {
            get; set;
        }

        public Avatar Avatar
        {
            get { return AvatarManager.Instance.GetAvatarById(AvatarData.Id); }
        }

        public bool IsOnline
        {
            get { return Avatar != null; }
        }

        public bool InRoom => (Avatar != null ? Avatar.RoomUser.Room != null : false);

        #endregion

        #region Constructors

        public MessengerUser(AvatarData friendData)
        {
            this.AvatarData = friendData;
        }

        #endregion

        #region Public methods

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as MessengerUser;

            if (t == null || t.AvatarData == null || this.AvatarData == null)
                return false;

            if (t.AvatarData.Id == this.AvatarData.Id)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
