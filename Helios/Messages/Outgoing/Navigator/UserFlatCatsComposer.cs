﻿using Helios.Storage.Database.Data;
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
            m_Data.Add(categories.Count);

            foreach (var category in categories)
            {
                m_Data.Add(category.Id);
                m_Data.Add(category.Caption);
                m_Data.Add(category.IsEnabled);
            }
        }
    }
}