using Helios.Storage.Models.Entity;
using System;

namespace Helios.Game
{
    public class Pet : IEntity
    {
        public IEntityData EntityData => throw new NotImplementedException();

        public RoomEntity RoomEntity => throw new NotImplementedException();
    }
}
