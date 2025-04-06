using System.Collections.Generic;
using Helios.Game;
using Helios.Storage.Models.Messenger;

namespace Helios.Messages.Outgoing
{
    internal class InitMessengerComposer : IMessageComposer
    {
        private int maxNormalFriends;
        private int maxHCFriends;
        private int maxVIPFriends;

        private List<MessengerCategoryData> categories;
        private List<MessengerUser> friends;

        public InitMessengerComposer(int maxNormalFriends, int maxHCFriends, int maxVIPFriends, List<MessengerCategoryData> categories, List<MessengerUser> friends)
        {
            this.maxNormalFriends = maxNormalFriends;
            this.maxHCFriends = maxHCFriends;
            this.maxVIPFriends = maxVIPFriends;

            this.categories = categories;
            this.friends = friends;
        }

        public override void Write()
        {
            // See https://habbo.fandom.com/wiki/Benefits_of_VIP
            _data.Add(maxNormalFriends); // HC            _SafeStr_10476():int
            _data.Add(0);
            _data.Add(maxHCFriends); // HC limit         _SafeStr_10477():int
            _data.Add(maxVIPFriends); // VIP limit        _local_2._SafeStr_10478
            _data.Add(categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                _data.Add(i);
                _data.Add(category.Label);
                i++;
            }

            _data.Add(friends.Count);
            foreach (var friend in friends)
            {
                _data.Add(friend.AvatarData.Id);
                _data.Add(friend.AvatarData.Name);
                _data.Add(1);
                _data.Add(friend.IsOnline);
                _data.Add(friend.IsOnline ? friend.InRoom : false);
                _data.Add(friend.AvatarData.Figure);
                _data.Add(0); // category id
                _data.Add(friend.AvatarData.Motto); // motto
                _data.Add(friend.AvatarData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss")); // unknown??
                _data.Add(friend.AvatarData.RealName); // real name
                //_data.Add(false);
                //_data.Add(false);
                //_data.Add(false); // is using pocket habbo
                //_data.Add((short)0); // relationship status
            }
        }
    }
}