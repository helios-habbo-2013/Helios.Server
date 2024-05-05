using Helios.Game;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class CataloguePagesComposer : IMessageComposer
    {
        private int rank;
        private bool hasClub;
        private List<CataloguePage> parentPages;

        public CataloguePagesComposer(int rank, bool hasClub)
        {
            this.rank = rank;
            this.hasClub = hasClub;
            this.parentPages = CatalogueManager.Instance.GetPages(-1, rank, hasClub);
        }

        public override void Write()
        {
            _data.Add(true);
            _data.Add(0);
            _data.Add(0);
            _data.Add(-1);
            _data.Add("root");
            _data.Add("");
            _data.Add(parentPages.Count);

            foreach (var childTab in parentPages)
            {
                AppendIndexNode(childTab);
                RecursiveIndexNode(childTab);
            }

            _data.Add(true);
        }

        private void RecursiveIndexNode(CataloguePage parentTab)
        {
            var childTabs = CatalogueManager.Instance.GetPages(parentTab.Data.Id, rank, hasClub);
            _data.Add(childTabs.Count);

            foreach (var childTab in childTabs)
            {
                AppendIndexNode(childTab);
                RecursiveIndexNode(childTab);
            }
        }

        private void AppendIndexNode(CataloguePage childTab)
        {
            _data.Add(true);
            _data.Add(childTab.Data.IconColour);
            _data.Add(childTab.Data.IconImage);
            _data.Add(childTab.Data.Id);
            _data.Add(childTab.Data.PageLink);
            _data.Add(childTab.Data.Caption);
        }
    }
}
