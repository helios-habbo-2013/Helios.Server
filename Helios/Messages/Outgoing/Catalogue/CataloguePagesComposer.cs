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
            this.AppendInt32(0);
            this.AppendInt32(0);
            this.AppendInt32(-1);
            this.AppendStringWithBreak("root");
            this.AppendBoolean(false);
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
            this.AppendInt32(childTabs.Count);

            foreach (var childTab in childTabs)
            {
                AppendIndexNode(childTab);
                RecursiveIndexNode(childTab);
            }
        }

        private void AppendIndexNode(CataloguePage childTab)
        {
            this.AppendBoolean(true);
            this.AppendInt32(childTab.Data.IconColour);
            this.AppendInt32(childTab.Data.IconImage);
            this.AppendInt32(childTab.Data.Id);
            this.AppendStringWithBreak(childTab.Data.Caption);
            this.AppendBoolean(false);
        }

        public override int HeaderId => 126;
    }
}
