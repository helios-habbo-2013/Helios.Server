using Helios.Game;
using Helios.Util.Extensions;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class UserUpdateMessageComposer : IMessageComposer
    {
        private List<IEntity> entities;

        public UserUpdateMessageComposer(List<IEntity> entities)
        {
            this.entities = entities;
        }

        public override void Write()
        {
            _data.Add(entities.Count);

            foreach (var entity in entities)
            {
                _data.Add(entity.RoomEntity.InstanceId);
                _data.Add(entity.RoomEntity.Position.X);
                _data.Add(entity.RoomEntity.Position.Y);
                _data.Add(entity.RoomEntity.Position.Z.ToClientValue());
                _data.Add(entity.RoomEntity.Position.HeadRotation);
                _data.Add(entity.RoomEntity.Position.BodyRotation);

                string statusString = "/";

                foreach (var kvp in entity.RoomEntity.Status)
                {
                    statusString += kvp.Key;

                    if (kvp.Value.Value.Length > 0)
                    {
                        statusString += " ";
                        statusString += kvp.Value.Value;
                    }

                    statusString += "/";
                }

                _data.Add(statusString);
            }

        }

        public override int HeaderId => 34;
    }
}