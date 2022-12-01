
namespace Helios.Storage.Database.Data
{
    public class PlayerSettingsData 
    {
        public virtual int UserId { get; set; }
        public virtual int Respect { get; set; }
        public virtual int DailyRespectPoints { get; set; }
        public virtual int DailyPetRespectPoints { get; set; }
        public virtual bool FriendRequestsEnabled { get; set; }
        public virtual bool FollowingEnabled { get; set; }
        public virtual long OnlineTime { get; set; }

        #region Constraints

        public virtual PlayerData PlayerData { get; set; }

        #endregion
    }
}
