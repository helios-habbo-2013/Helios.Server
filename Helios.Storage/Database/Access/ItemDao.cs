using Helios.Storage.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Database.Access
{
    public class ItemDao
    {
        /// <summary>
        /// Get list of all definition data
        /// </summary>
        public static List<ItemDefinitionData> GetDefinitions()
        {
            using (var context = new StorageContext())
            {
                return context.ItemDefinitionData.ToList();
            }

        }

        /// <summary>
        /// Get list of all item data for user inventory
        /// </summary>
        public static List<ItemData> GetUserItems(int userId)
        {
            using (var context = new StorageContext())
            {
                return context.ItemData
                    .Include(x => x.OwnerData)
                    .Where(x => x.OwnerId == userId).ToList();
            }
        }

        /// <summary>
        /// Get room items
        /// </summary>
        public static List<ItemData> GetRoomItems(int roomId)
        {
            using (var context = new StorageContext())
            {
                return context.ItemData
                    .Include(x => x.OwnerData)
                    .Where(x => x.RoomId == roomId)
                    .ToList();
            }
        }

        /// <summary>
        /// Get single item
        /// </summary>
        /// <returns>the item</returns>
        public static ItemData GetItem(string itemId)
        {
            using (var context = new StorageContext())
            {
                return context.ItemData
                    .Include(x => x.OwnerData)
                    .SingleOrDefault(x => x.Id == itemId);
            }
        }

        /// <summary>
        /// Save item definition
        /// </summary>
        /// <param name="itemDefinition"></param>
        public static void SaveDefinition(ItemDefinitionData itemDefinition)
        {
            using (var context = new StorageContext())
            {
                context.Update(itemDefinition);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Save item data
        /// </summary>
        public static void SaveItem(ItemData item)
        {
            using (var context = new StorageContext())
            {
                context.Update(item);
                context.SaveChanges();
                context.Entry(item).Reference(x => x.OwnerData).Load();
            }
        }


        /// <summary>
        /// Create items and refresh it with their filled in database ID's
        /// </summary>
        public static void CreateItems(List<ItemData> items)
        {
            using (var context = new StorageContext())
            {
                context.AddRange(items);
                context.SaveChanges();

                foreach (var item in items)
                    context.Entry(item).Reference(x => x.OwnerData).Load();

            }
        }

        /// <summary>
        /// Create item and refresh it with their filled in database ID's
        /// </summary>
        public static void CreateItem(ItemData item)
        {
            using (var context = new StorageContext())
            {
                context.Add(item);
                context.SaveChanges();
                context.Entry(item).Reference(x => x.OwnerData).Load();
            }
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="item"></param>
        public static void DeleteItem(ItemData item)
        {
            using (var context = new StorageContext())
            {
                context.ItemData.Remove(item); 
                context.SaveChanges();
            }
        }
    }
}
