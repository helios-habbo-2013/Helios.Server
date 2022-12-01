using Helios.Storage.Database.Data;
using System;

namespace Helios.Game
{
    public class Bot : IEntity
    {
        public IEntityData EntityData => throw new NotImplementedException();

        public RoomEntity RoomEntity => throw new NotImplementedException();
    }
}
