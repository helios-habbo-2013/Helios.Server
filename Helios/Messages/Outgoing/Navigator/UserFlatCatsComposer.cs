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
            AppendInt32(categories.Count);

            foreach (var category in categories)
            {
                AppendBoolean(true);
                AppendInt32(category.Id);
                AppendStringWithBreak(category.Caption);
            }

            _data.Add(string.Empty);
        }
    }
}