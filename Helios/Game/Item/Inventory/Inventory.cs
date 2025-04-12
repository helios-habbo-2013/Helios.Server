using Helios.Messages.Outgoing;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Util.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Game
{
    public class Inventory : ILoadable
    {
        #region Fields

        private Avatar avatar;

        #endregion

        #region Properties

        public ConcurrentDictionary<int, Item> Items;

        #endregion

        #region Constructors

        public Inventory(Avatar avatar)
        {
            this.avatar = avatar;
        }

        public void Load()
        {
            using (var context = new StorageContext())
            {
                Items = new ConcurrentDictionary<int, Item>(context.GetInventoryItems(avatar.Details.Id).Select(x => new Item(x)).ToDictionary(x => x.Id, x => x));
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Retrieve item from inventory
        /// </summary>
        public Item GetItem(int itemId)
        {
            if (Items.TryGetValue(itemId, out var item))
                return item;

            return null;
        }

        /// <summary>
        /// Get item by database id
        /// </summary>
        internal Item GetItem(string itemId)
        {
            return Items.Values.Where(x => x.Data.Id.ToString() == itemId).FirstOrDefault();
        }

        /// <summary>
        /// Add item to inventory
        /// </summary>
        public void AddItem(Item item, bool alertNewItem = false, bool forceUpdate = false)
        {
            this.Items.TryAdd(item.Id, item);
        }

        /// <summary>
        /// Remove item from inventory
        /// </summary>
        public void RemoveItem(Item item)
        {
            Items.Remove(item.Id);
            avatar.Send(new FurniListRemoveComposer(item.Id));
        }

        #endregion
    }
}
