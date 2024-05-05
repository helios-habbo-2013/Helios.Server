using System.Collections.Generic;
using Helios.Game;
using Helios.Util.Extensions;

namespace Helios.Messages.Outgoing
{
    public class SlideObjectBundleComposer : IMessageComposer
    {
        private Item roller;
        private List<RollingData> rollingItems;
        private RollingData rollingEntity;

        public SlideObjectBundleComposer(Item roller, List<RollingData> rollingItems, RollingData rollingEntity)
        {
            this.roller = roller;
            this.rollingItems = rollingItems;
            this.rollingEntity = rollingEntity;
        }

        public override void Write()
        {
            m_Data.Add(roller.Position.X);
            m_Data.Add(roller.Position.Y);

            m_Data.Add(roller.Position.GetSquareInFront().X);
            m_Data.Add(roller.Position.GetSquareInFront().Y);

            m_Data.Add(rollingItems.Count);

            foreach (var item in rollingItems)
            {
                m_Data.Add(item.Item.Id);
                m_Data.Add(item.FromPosition.Z.ToClientValue());
                m_Data.Add(item.NextPosition.Z.ToClientValue());
            }

            bool hasEntity = rollingEntity != null && rollingEntity.Entity.RoomEntity.Room != null;

            m_Data.Add(roller.Id);
            m_Data.Add(hasEntity ? 2 : 0);

            if (hasEntity)
            {
                m_Data.Add(rollingEntity.Entity.RoomEntity.InstanceId);
                m_Data.Add(rollingEntity.FromPosition.Z.ToClientValue());
                m_Data.Add(rollingEntity.DisplayHeight.ToClientValue());
            }
        }
    }
}
