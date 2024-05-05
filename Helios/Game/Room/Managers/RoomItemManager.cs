﻿using Helios.Storage.Access;
using Helios.Util.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Helios.Game.Managers
{
    public class RoomItemManager : ILoadable
    {
        #region Fields

        private Room room;

        #endregion

        #region Properties

        public ConcurrentDictionary<int, Item> Items { get; set; }

        #endregion

        #region Constructors

        public RoomItemManager(Room room)
        {
            this.room = room;
        }

        #endregion

        #region Public methods
        
        public void Load()
        {
            Items = new ConcurrentDictionary<int, Item>();

            foreach (var itemData in ItemDao.GetRoomItems(room.Data.Id))
            {
                Item item = new Item(itemData);
                Items.TryAdd(item.Id, item);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Retrieve item from room item list
        /// </summary>
        public Item GetItem(int itemId)
        {
            if (Items.TryGetValue(itemId, out var item))
                return item;

            return null;
        }


        /// <summary> 
        /// Retrieve item from room item list by database ID
        /// </summary>
        public Item GetItem(string itemId)
        {
            return Items.Values.Where(x => x.Data.Id.ToString() == itemId).FirstOrDefault();
        }

        /// <summary>
        /// Add item to manager
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            Items.TryAdd(item.Id, item);
        }

        /// <summary>
        /// Remove item from manager
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            Items.Remove(item.Id);
        }

        /// <summary>
        /// Get if room has item where matching parameters
        /// </summary>
        public bool HasItem(Func<Item, bool> predicate)
        {
            return Items.Values.Where(x => x.Definition != null).Count(predicate) > 0;
        }

        #endregion
    }
}
