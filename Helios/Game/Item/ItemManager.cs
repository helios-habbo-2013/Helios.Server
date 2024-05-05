﻿using Helios.Storage.Access;
using Helios.Storage.Models.Item;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Helios.Game
{
    public class ItemManager : ILoadable
    {
        #region Fields

        public static readonly ItemManager Instance = new ItemManager();
        private int ItemCounter;
        private int EffectCounter;

        #endregion

        #region Properties

        public Dictionary<int, ItemDefinition> Definitions { get; set; }

        #endregion

        #region Constructors

        public void Load()
        {
            ItemCounter = 1;
            Definitions = ItemDao.GetDefinitions().Select(x => new ItemDefinition(x)).ToDictionary(x => x.Data.Id, x => x);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Safe method to try and get definition
        /// </summary>
        public ItemDefinition GetDefinition(int? definitionId)
        {
            if (definitionId != null)
            {
                Definitions.TryGetValue(definitionId.Value, out var definition);
                return definition;
            }

            return null;
        }

        /// <summary>
        /// Generate client side ID for item
        /// </summary>
        public int GenerateId()
        {
            return Interlocked.Increment(ref ItemCounter);
        }

        /// <summary>
        /// Generate client side ID for effect
        /// </summary>
        public int GenerateEffectId()
        {
            return Interlocked.Increment(ref EffectCounter);
        }

        /// <summary>
        /// Try and resolve an item inside a room or persons inventory
        /// </summary>
        /// <param name="itemId">the item GUID</param>
        /// <returns>the tiem</returns>
        public Item ResolveItem(string itemId = null, ItemData itemData = null)
        {
            if (itemData == null)
                itemData = ItemDao.GetItem(itemId);

            if (itemData == null || itemData.RoomId == null)
                return null;

            if (itemData.RoomId is int roomId)
            {
                var room = RoomManager.Instance.GetRoom(roomId);

                if (room == null)
                {
                    var avatar = AvatarManager.Instance.GetAvatarById(roomId);

                    if (avatar != null)
                    {
                        return avatar.Inventory.GetItem(itemData.Id.ToString());
                    }
                }
                else
                {
                    if (room.ItemManager.Items == null)
                        room.ItemManager.Load();

                    return room.ItemManager.GetItem(itemData.Id.ToString());
                }
            }

            return null;
        }

        #endregion
    }
}
