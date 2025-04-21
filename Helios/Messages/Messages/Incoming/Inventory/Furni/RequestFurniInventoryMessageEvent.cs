using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Incoming
{
    class RequestFurniInventoryMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var inventoryItems = new List<Item>(avatar.Inventory.Items.Values);

            /*int itemsPerPage = ValueManager.Instance.GetInt("inventory.items.per.page");
            
            int pages = inventoryItems.CountPages(itemsPerPage);

            for (int i = 0; i < pages; i++)
            {
                avatar.Send(new InventoryMessageComposer(pages, i, inventoryItems.GetPage(i, itemsPerPage)));
            }*/


            avatar.Send(new FurniListComposer("S", inventoryItems.ToList()));

            //avatar.Send(new InventoryMessageComposer("S", inventoryItems.Where(x => x.Definition.Type == "S").ToList()));
            //avatar.Send(new InventoryMessageComposer("I", inventoryItems.Where(x => x.Definition.Type == "I").ToList()));
        }

        public int HeaderId => 404;
    }
}