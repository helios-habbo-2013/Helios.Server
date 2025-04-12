using Helios.Storage.Models.Navigator;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class UserFlatCatsComposer : IMessageComposer
    {
        public List<NavigatorCategoryData> categories;

        public UserFlatCatsComposer(List<NavigatorCategoryData> categories)
        {
            this.categories = categories;
        }

        public override void Write()
        {
            this.AppendInt32(categories.Count);

            foreach (var category in categories)
            {
                if (category.Id > 0)
                {
                    this.AppendBoolean(true);
                }

                this.AppendInt32(category.Id);
                this.AppendStringWithBreak(category.Caption);
            }
        }

        public override int HeaderId => 221;
    }
}