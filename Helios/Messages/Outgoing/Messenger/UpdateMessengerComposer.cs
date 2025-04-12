using Helios.Game;
using Helios.Storage.Models.Messenger;
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
            _data.Add(categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                _data.Add(i);
                _data.Add(category.Label);
                i++;
            }

            _data.Add(updates.Count);
            foreach (var messengerUpdate in updates)
            {
                _data.Add((int)messengerUpdate.UpdateType);

                switch (messengerUpdate.UpdateType)
                {
                    case MessengerUpdateType.RemoveFriend:
                        {
                            _data.Add(messengerUpdate.Friend.AvatarData.Id);
                            break;
                        }
                    case MessengerUpdateType.AddFriend:
                    case MessengerUpdateType.UpdateFriend:
                        {
                            _data.Add(messengerUpdate.Friend.AvatarData.Id);
                            _data.Add(messengerUpdate.Friend.AvatarData.Name);
                            _data.Add(1);
                            _data.Add(messengerUpdate.Friend.IsOnline);
                            _data.Add(messengerUpdate.Friend.InRoom);
                            _data.Add(messengerUpdate.Friend.IsOnline ? messengerUpdate.Friend.AvatarData.Figure : "");
                            _data.Add(0); // category id
                            _data.Add(messengerUpdate.Friend.AvatarData.Motto); // motto
                            _data.Add(messengerUpdate.Friend.AvatarData.RealName); // real name
                            _data.Add(messengerUpdate.Friend.AvatarData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss")); // unknown??
                            //_data.Add(false);
                            //_data.Add(false);
                            //_data.Add(false);
                            //_data.Add((short)0); // relationship status
                            break;
                        }
                }
            }
        }

        public override int HeaderId => 12;
    }
}