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
            this.AppendInt32(maxNormalFriends); // HC            _SafeStr_10476():int
            this.AppendInt32(200);
            this.AppendInt32(maxHCFriends); // HC limit         _SafeStr_10477():int
            this.AppendInt32(maxVIPFriends); // VIP limit        _local_2._SafeStr_10478
            this.AppendInt32(categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                _data.Add(i);
                _data.Add(category.Label);
                i++;
            }

            this.AppendInt32(friends.Count);
            foreach (var friend in friends)
            {
                this.AppendInt32(friend.AvatarData.Id);
                _data.Add(friend.AvatarData.Name);
                this.AppendInt32(1);
                _data.Add(friend.IsOnline);
                _data.Add(friend.IsOnline ? friend.InRoom : false);
                _data.Add(friend.AvatarData.Figure);
                this.AppendInt32(0); // category id
                _data.Add(friend.AvatarData.Motto); // motto
                _data.Add(friend.AvatarData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss")); // unknown??
                _data.Add(friend.AvatarData.RealName); // real name
                //_data.Add(false);
                //_data.Add(false);
                //_data.Add(false); // is using pocket habbo
                //_data.Add((short)0); // relationship status
            }
        }

        public override int HeaderId => 12;
    }
}