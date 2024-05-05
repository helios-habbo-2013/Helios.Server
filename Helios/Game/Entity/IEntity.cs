using Helios.Storage.Models.Entity;

namespace Helios.Game
{
    public interface IEntity
    {
        IEntityData EntityData { get; }

        RoomEntity RoomEntity { get; }
    }
}
