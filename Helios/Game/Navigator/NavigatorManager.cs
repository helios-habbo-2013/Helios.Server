using System.Collections.Generic;
using System.Linq;
using Helios.Util.Extensions;
using Helios.Storage.Access;
using Helios.Storage.Models.Navigator;
using Helios.Storage;

namespace Helios.Game
{
    public class NavigatorManager : ILoadable
    {
        #region Fields

        public static readonly NavigatorManager Instance = new NavigatorManager();

        #endregion

        #region Properties

        public List<NavigatorCategoryData> Categories;

        #endregion

        #region Constructors

        public void Load()
        {
            using (var context = new StorageContext())
            {
                Categories = context.GetCategories();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get applicable categories for rank
        /// </summary>
        public List<NavigatorCategoryData> GetCategories(int rank)
        {
            return [.. Categories.Where(x => rank >= x.MinimumRank)];
        }

        #endregion

        #region Promotion

        /// <summary>
        /// Get a random popular promotion
        /// </summary>
        /// <returns></returns>
        public static PublicItemData GetPopularPromotion()
        {
            using var context = new StorageContext();

            var publicItemsList = context.GetPublicItems().Where(x => x.Room != null && x.Room.UsersNow > 0).ToList();

            if (publicItemsList.Count > 0)
                return publicItemsList.PickRandom();

            return null;
        }

        #endregion
    }
}
