﻿using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class FurniListAddComposer : IMessageComposer
    {
        public Item item;

        public FurniListAddComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            InventoryMessageComposer.Serialize(this, item);
        }
    }
}
