using Helios.Storage.Database.Data;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Database.Access
{
    public class CatalogueDao
    {
        /// <summary>
        /// Get page data by page id
        /// </summary>
        public static CataloguePageData GetPageData(int id)
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CataloguePageData>().Where(x => x.Id == id).Take(1).SingleOrDefault();
            }*/

            using (var context = new StorageContext())
            {
                return context.CataloguePageData.SingleOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Get all page data
        /// </summary>
        /// <returns></returns>
        public static List<CataloguePageData> GetPages()
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CataloguePageData>().List() as List<CataloguePageData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.CataloguePageData.ToList();
            }
        }

        /// <summary>
        /// Get all catalogue item data
        /// </summary>
        /// <returns></returns>
        public static List<CatalogueItemData> GetItems()
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CatalogueItemData>().List() as List<CatalogueItemData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.CatalogueItemData.ToList();
            }
        }

        /// <summary>
        /// Get all item packages
        /// </summary>
        /// <returns></returns>
        public static List<CataloguePackageData> GetPackages()
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CataloguePackageData>().List() as List<CataloguePackageData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.CataloguePackageData.ToList();
            }
        }

        /// <summary>
        /// Get all discount data
        /// </summary>
        /// <returns></returns>
        public static List<CatalogueDiscountData> GetDiscounts()
        {
            using (var context = new StorageContext())
            {
                return context.CatalogueDiscountData.ToList();
            }
        }

        /// <summary>
        /// Get subscription data by page id
        /// </summary>
        public static List<CatalogueSubscriptionData> GetSubscriptionData()
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                return session.QueryOver<CatalogueSubscriptionData>().List() as List<CatalogueSubscriptionData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.CatalogueSubscriptionData.ToList();
            }
        }
    }
}
