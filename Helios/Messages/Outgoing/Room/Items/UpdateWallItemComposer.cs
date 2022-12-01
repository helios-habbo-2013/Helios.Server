using System;
using System.Collections.Generic;
using System.Text;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class UpdateWallItemComposer : IMessageComposer
    {
        private Item item;

        public UpdateWallItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            WallItemsComposer.Serialize(this, item);

            m_Data.Add(item.Data.OwnerData.Id);
            m_Data.Add(item.Data.OwnerData.Name);
        }
    }
}
