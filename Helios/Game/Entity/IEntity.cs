using Helios.Storage.Database.Data;

namespace Helios.Game
{
    public interface IEntity
    {
        IEntityData EntityData { get; }

        RoomEntity RoomEntity { get; }
    }
}
