
namespace Helios.Storage.Database.Data
{
    public class TagData
    {
        public virtual int UserId { get; set; }
        public virtual int RoomId { get; set; }
        public virtual string Text { get; set; }

        #region Constraints

        public virtual RoomData RoomData { get; set; }

        #endregion
    }
}
