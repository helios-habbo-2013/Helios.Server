using Helios.Game;
using Helios.Util.Extensions;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    public class UsersComposer : IMessageComposer
    {
        private List<IEntity> entities;

        public UsersComposer(List<IEntity> entities)
        {
            this.entities = entities;
        }

        public override void Write()
        {
            m_Data.Add(entities.Count);

            foreach (var entity in entities)
            {
                if (entity is Avatar avatar)
                {
                    m_Data.Add(avatar.Details.Id);
                    m_Data.Add(avatar.Details.Name);
                    m_Data.Add(avatar.Details.Motto);
                    m_Data.Add(avatar.Details.Figure);
                    m_Data.Add(avatar.RoomUser.InstanceId);
                    m_Data.Add(avatar.RoomUser.Position.X);
                    m_Data.Add(avatar.RoomUser.Position.Y);
                    m_Data.Add(avatar.RoomUser.Position.Z.ToClientValue());
                    m_Data.Add(avatar.RoomUser.Position.BodyRotation);
                    m_Data.Add(1);
                    m_Data.Add(avatar.Details.Sex.ToLower());

                    // TODO: Group shit for later
                    m_Data.Add(-1);
                    m_Data.Add(-1);
                    m_Data.Add(0);

                    m_Data.Add(avatar.Details.AchievementPoints);
                }
            }
        }
    }
}
