using Helios.Storage.Models.Navigator;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class PublicItemsComposer : IMessageComposer
    {
        public List<PublicItemData> publicItems;

        public PublicItemsComposer(List<PublicItemData> publicItems)
        {
            this.publicItems = publicItems;
        }

        public override void Write()
        {
            _data.Add(publicItems.Count);

            foreach (var item in publicItems)
                Compose(this, item);

            _data.Add(0);
            _data.Add(0);
        }
        
        public static void Compose(IMessageComposer messageComposer, PublicItemData publicItem)
        {
            messageComposer.Data.Add(publicItem.BannerId);

            messageComposer.Data.Add(publicItem.Label);
            messageComposer.Data.Add(publicItem.Description);

            messageComposer.Data.Add(publicItem.ThumbnailLayout); // is category expanded?

            messageComposer.Data.Add(publicItem.Label);
            messageComposer.Data.Add(publicItem.Image);
            messageComposer.Data.Add(publicItem.ParentId);
            messageComposer.Data.Add(publicItem.Room != null ? publicItem.Room.UsersNow : 0);
            messageComposer.Data.Add((int)publicItem.BannerType);

            if (publicItem.BannerType == BannerType.TAG)
                messageComposer.Data.Add(string.Empty); // Tag to search

            //if (publicItem.BannerType == BannerType.FLAT)
            //    FlatListComposer.Compose(messageComposer, publicItem.Room);

            if (publicItem.BannerType == BannerType.PUBLIC_FLAT)
            {
                messageComposer.Data.Add(publicItem.Description);
                messageComposer.Data.Add(publicItem.ParentId);
                messageComposer.Data.Add(publicItem.ParentId);
                messageComposer.Data.Add(publicItem.Room.CCTs);
                messageComposer.Data.Add(publicItem.ParentId);
                messageComposer.Data.Add(publicItem.Room.Id);
                /*
                messageComposer.Data.Add(publicItem.Room.CCTs);
                messageComposer.Data.Add(publicItem.Room.UsersMax);
                messageComposer.Data.Add(publicItem.Room.Id);
                */
            }
        }
    }
}