namespace Helios.Storage.Database.Data
{
    public class PublicItemData
    {
        public virtual int BannerId { get; set; }
        public virtual int ParentId { get; set; }
        public virtual BannerType BannerType { get; set; }
        public virtual int RoomId { get; set; }
        public virtual ImageType ImageType { get; set; }
        public virtual string Label { get; set; }
        public virtual string Description { get; set; }
        public virtual int DescriptionEntry { get; set; }
        public virtual string Image { get; set; }

        #region Constraints

        public virtual RoomData Room { get; set; }

        #endregion
    }

    public enum BannerType
    {
        // 'NONE','TAG','FLAT','PUBLIC_FLAT','CATEGORY'
        NONE = 0,
        TAG = 1,
        FLAT = 2,
        PUBLIC_FLAT = 3,
        CATEGORY = 4
    }

    public enum ImageType
    {
        INTERNAL = 0,
        EXTERNAL = 1
    }
}
