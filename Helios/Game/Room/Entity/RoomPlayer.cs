namespace Helios.Game
{
    public class RoomAvatar : RoomEntity
    {
        #region Fields

        #endregion

        #region Properties

        public Avatar Avatar { get; private set; }

        #endregion

        #region Constructors

        public RoomAvatar(Avatar avatar) : base((IEntity)avatar)
        {
            Avatar = avatar;
            TaskObject = new AvatarTaskObject(avatar);
        }

        #endregion
    }
}
