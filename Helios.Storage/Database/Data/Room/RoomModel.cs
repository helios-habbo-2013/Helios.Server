namespace Helios.Storage.Database.Data
{
    public class RoomModelData
    {
        public virtual int Id { get; set; }
        public virtual string Model { get; set; }
        public virtual int DoorX { get; set; }
        public virtual int DoorY { get; set; }
        public virtual int DoorZ { get; set; }
        public virtual int DoorDirection { get; set; }
        public virtual string Heightmap { get; set; }
        public virtual bool IsClubOnly { get; set; }
    }
}
