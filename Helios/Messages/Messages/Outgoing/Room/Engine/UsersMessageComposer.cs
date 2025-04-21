using Helios.Game;
using Helios.Util.Extensions;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class UsersMessageComposer : IMessageComposer
    {
        private List<IEntity> _entities;

        public UsersMessageComposer(List<IEntity> entities)
        {
            this._entities = entities;
        }

        public override void Write()
        {
            _data.Add(_entities.Count);

            foreach (var entity in _entities)
            {
                if (entity is Avatar avatar)
                {
                    _data.Add(avatar.Details.Id);
                    _data.Add(avatar.Details.Name);
                    _data.Add(avatar.Details.Motto);
                    _data.Add(avatar.Details.Figure);
                    _data.Add(avatar.RoomUser.InstanceId);
                    _data.Add(avatar.RoomUser.Position.X);
                    _data.Add(avatar.RoomUser.Position.Y);
                    _data.Add(avatar.RoomUser.Position.Z.ToClientValue());
                    _data.Add(avatar.RoomUser.Position.BodyRotation);
                    _data.Add(1);
                    _data.Add(avatar.Details.Sex.ToLower());

                    // TODO: Group shit for later

                    if (avatar.Details.FavouriteGroup != null)
                    {
                        _data.Add(avatar.Details.FavouriteGroup.Id);
                        _data.Add(0);
                        _data.Add(avatar.Details.FavouriteGroup.Name);
                        _data.Add(avatar.Details.FavouriteGroup.Badge);
                    }
                    else
                    {
                        _data.Add(-1);
                        _data.Add(-1);
                        _data.Add(0);
                    }

                    _data.Add(avatar.Details.AchievementPoints);
                }
            }
        }

        public override int HeaderId => 28;
    }
}