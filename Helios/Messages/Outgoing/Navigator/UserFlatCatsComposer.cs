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
            _data.Add(categories.Count);

            foreach (var category in categories)
            {
                _data.Add(category.Id);
                _data.Add(category.Caption);
                _data.Add(category.IsEnabled);
            }
        }
    }
}