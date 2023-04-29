using Helios.Game;
using Helios.Storage.Database.Data;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    internal class UpdateMessengerComposer : IMessageComposer
    {
        private List<MessengerCategoryData> categories;
        private List<MessengerUpdate> updates;

        public UpdateMessengerComposer(List<MessengerCategoryData> categories, List<MessengerUpdate> updates)
        {
            this.categories = categories;
            this.updates = updates;
        }

        public override void Write()
        {
            m_Data.Add(categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                m_Data.Add(i);
                m_Data.Add(category.Label);
                i++;
            }

            m_Data.Add(updates.Count);
            foreach (var messengerUpdate in updates)
            {
                m_Data.Add((int)messengerUpdate.UpdateType);

                switch(messengerUpdate.UpdateType)
                {
                    case MessengerUpdateType.RemoveFriend:
                        {
                            m_Data.Add(messengerUpdate.Friend.AvatarData.Id);
                            break;
                        }
                    case MessengerUpdateType.AddFriend:
                    case MessengerUpdateType.UpdateFriend:
                        {
                            m_Data.Add(messengerUpdate.Friend.AvatarData.Id);
                            m_Data.Add(messengerUpdate.Friend.AvatarData.Name);
                            m_Data.Add(1);
                            m_Data.Add(messengerUpdate.Friend.IsOnline);
                            m_Data.Add(messengerUpdate.Friend.InRoom);
                            m_Data.Add(messengerUpdate.Friend.IsOnline ? messengerUpdate.Friend.AvatarData.Figure : "");
                            m_Data.Add(0); // category id
                            m_Data.Add(messengerUpdate.Friend.AvatarData.Motto); // motto
                            m_Data.Add(messengerUpdate.Friend.AvatarData.RealName); // real name
                            m_Data.Add(messengerUpdate.Friend.AvatarData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss")); // unknown??
                            m_Data.Add(false);
                            m_Data.Add(false);
                            m_Data.Add(false);
                            m_Data.Add((short)0); // relationship status
                            break;
                        }
                }
            }
        }
    }
}