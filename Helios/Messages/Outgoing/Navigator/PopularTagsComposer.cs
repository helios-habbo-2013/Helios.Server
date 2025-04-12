using Helios.Storage.Models.Misc;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class PopularTagsComposer : IMessageComposer
    {
        private List<PopularTag> popularTags;

        public PopularTagsComposer(List<PopularTag> lists)
        {
            this.popularTags = lists;
        }

        public override void Write()
        {
            _data.Add(this.popularTags.Count);

            foreach (var tag in this.popularTags)
            {
                _data.Add(tag.Tag);
                _data.Add(tag.Quantity);
            }
        }

        public override int HeaderId => 452;
    }
}