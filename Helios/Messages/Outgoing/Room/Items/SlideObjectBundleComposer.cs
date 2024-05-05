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
            _data.Add(roller.Position.X);
            _data.Add(roller.Position.Y);

            _data.Add(roller.Position.GetSquareInFront().X);
            _data.Add(roller.Position.GetSquareInFront().Y);

            _data.Add(rollingItems.Count);

            foreach (var item in rollingItems)
            {
                _data.Add(item.Item.Id);
                _data.Add(item.FromPosition.Z.ToClientValue());
                _data.Add(item.NextPosition.Z.ToClientValue());
            }

            bool hasEntity = rollingEntity != null && rollingEntity.Entity.RoomEntity.Room != null;

            _data.Add(roller.Id);
            _data.Add(hasEntity ? 2 : 0);

            if (hasEntity)
            {
                _data.Add(rollingEntity.Entity.RoomEntity.InstanceId);
                _data.Add(rollingEntity.FromPosition.Z.ToClientValue());
                _data.Add(rollingEntity.DisplayHeight.ToClientValue());
            }
        }
    }
}
