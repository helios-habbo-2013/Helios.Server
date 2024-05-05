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
            m_Data.Add(this.popularTags.Count);

            foreach (var tag in this.popularTags)
            {
                m_Data.Add(tag.Tag);
                m_Data.Add(tag.Quantity);
            }
        }
    }
}