using System.Collections.Generic;
using System.Linq;
using Helios.Storage.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace Helios.Storage.Database.Access
{
    public class NavigatorDao
    {
        /// <summary>
        /// Get list of public items
        /// </summary>
        public static List<PublicItemData> GetPublicItems()
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<PublicItemData>().List() as List<PublicItemData>;//.Where(x => x.Room != null).ToList();
            //}

            using (var context = new StorageContext())
            {
                return context.PublicItemData
                    .Include(x => x.Room).ThenInclude(x => x.Category)
                    .Include(x => x.Room).ThenInclude(x => x.OwnerData)
                    .ToList();
            }
        }

        /// <summary>
        /// Get list of room categories
        /// </summary>
        public static List<NavigatorCategoryData> GetCategories()
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<NavigatorCategoryData>().List() as List<NavigatorCategoryData>;
            //}

            using (var context = new StorageContext())
            {
                return context.NavigatorCategoryData.ToList();
            }
        }
    }
}
