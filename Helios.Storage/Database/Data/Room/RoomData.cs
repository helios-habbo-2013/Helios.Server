using System.Collections.Generic;

namespace Helios.Storage.Database.Data
{
    public class RoomData
    {
        public virtual int Id { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual RoomStatus Status { get; set; }
        public virtual string Password { get; set; }
        public virtual int ModelId { get; set; }
        public virtual string CCTs { get; set; }
        public virtual string Wallpaper { get; set; }
        public virtual string Floor { get; set; }
        public virtual string Landscape { get; set; }
        public virtual bool AllowPets { get; set; }
        public virtual bool AllowPetsEat { get; set; }
        public virtual bool AllowWalkthrough { get; set; }
        public virtual bool IsHidingWall { get; set; }
        public virtual int WallThickness { get; set; }
        public virtual int FloorThickness { get; set; }
        public virtual int Rating { get; set; }
        public virtual bool IsPrivateRoom => OwnerId > 0;
        public virtual bool IsPublicRoom => OwnerId == 0;
        public virtual int UsersNow { get; set; }
        public virtual int UsersMax { get; set; }
        public virtual bool IsMuted { get; set; }
        public virtual bool IsOwnerHidden { get; set; }
        public virtual int TradeSetting { get; set; }
        public virtual RoomMuteSetting WhoCanMute { get; set; }
        public virtual RoomKickSetting WhoCanKick { get; set; }
        public virtual RoomBanSetting WhoCanBan { get; set; }


        #region Constraints

        public virtual PlayerData OwnerData { get; set; }
        public virtual NavigatorCategoryData Category { get; set; }
        public virtual List<TagData> Tags { get; set; }
        public virtual PublicItemData PublicItem { get; set; }

        #endregion

        public static RoomStatus ToStatusEnum(int roomAccess)
        {
            switch (roomAccess)
            {
                case 1:
                    return RoomStatus.CLOSED;
                case 2:
                    return RoomStatus.PASSWORD;
                default:
                    return RoomStatus.OPEN;

            }
        }
    }

    public enum RoomStatus
    {
        OPEN = 0,
        CLOSED = 1,
        PASSWORD = 2
    }

    public enum RoomMuteSetting
    {
        NONE = 0,
        USERS_WITH_RIGHTS = 1,
    }

    public enum RoomBanSetting
    {
        NONE = 0,
        USERS_WITH_RIGHTS = 1,
        ALL_USERS = 2
    }

    public enum RoomKickSetting
    {
        NONE = 0,
        USERS_WITH_RIGHTS = 1,
        ALL_USERS = 2
    }
}
