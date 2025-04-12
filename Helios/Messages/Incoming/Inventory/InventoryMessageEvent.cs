using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Util.Extensions;
using System.Collections.Generic;

namespace Helios.Messages.Incoming
{
    class InventoryMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var inventoryItems = new List<Item>(avatar.Inventory.Items.Values);

            int itemsPerPage = ValueManager.Instance.GetInt("inventory.items.per.page");
            int pages = inventoryItems.CountPages(itemsPerPage);

            for (int i = 0; i < pages; i++)
            {
                avatar.Send(new InventoryMessageComposer(pages, i, inventoryItems.GetPage(i, itemsPerPage)));
            }
        }

        public int HeaderId => -1;
    }
}
