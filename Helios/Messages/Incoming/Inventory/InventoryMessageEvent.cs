using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Util.Extensions;
using System;
using System.Collections.Generic;

namespace Helios.Messages.Incoming
{
    class InventoryMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var inventoryItems = new List<Item>(player.Inventory.Items.Values);

            int itemsPerPage = ValueManager.Instance.GetInt("inventory.items.per.page");
            int pages = inventoryItems.CountPages(itemsPerPage);

            for (int i = 0; i < pages; i++)
            {
                player.Send(new InventoryMessageComposer(pages, i, inventoryItems.GetPage(i, itemsPerPage)));
            }
        }
    }
}
