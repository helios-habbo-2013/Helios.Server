using System.Collections.Generic;
using System.Linq;
using Helios.Util.Extensions;
using Helios.Storage.Access;
using Helios.Storage.Models.Navigator;
using Helios.Storage;
using Serilog;

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
            Log.ForContext<NavigatorManager>().Information("Loading Navigator Categories");

            using (var context = new StorageContext())
            {
                Categories = context.GetCategories();
            }

            Log.ForContext<NavigatorManager>().Information("Loaded {Count} of Navigator Categories", Categories.Count);
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
    }
}
