using Helios.Game;
using Helios.Storage.Database.Data;
using Helios.Util.Extensions;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;

namespace Helios.Messages.Outgoing
{
    public class FloorItemComposer : IMessageComposer
    {
        private Item floorItem;

        public FloorItemComposer(Item floorItem)
        {
            this.floorItem = floorItem;
        }

        public override void Write()
        {
            FloorItemsComposer.Serialize(this, floorItem);

            m_Data.Add(floorItem.Data.OwnerData.Id);
            m_Data.Add(floorItem.Data.OwnerData.Name);
        }
    }
}
