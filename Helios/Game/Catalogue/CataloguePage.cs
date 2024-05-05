using Helios.Storage.Models.Catalogue;
using System.Collections.Generic;

namespace Helios.Game
{
    public class CataloguePage
    {
        #region Properties

        public CataloguePageData Data { get; }
        public List<CatalogueItem> Items => CatalogueManager.Instance.GetItems(Data.Id);
        public List<string> Images { get; internal set; }
        public List<string> Texts { get; internal set; }

        #endregion

        #region Constructors

        public CataloguePage(CataloguePageData data)
        {
            Data = data;
        }

        #endregion

        #region Public methods


        #endregion
    }
}
