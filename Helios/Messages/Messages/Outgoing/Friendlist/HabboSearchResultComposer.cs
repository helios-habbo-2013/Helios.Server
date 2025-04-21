using Helios.Game;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class HabboSearchResultComposer : IMessageComposer
    {
        private List<MessengerUser> friends;
        private List<MessengerUser> users;

        public HabboSearchResultComposer(List<MessengerUser> friends, List<MessengerUser> users)
        {
            this.friends = friends;
            this.users = users;
        }

        public override void Write()
        {
            _data.Add(friends.Count);

            foreach (var user in friends)
                Serialise(user);

            _data.Add(users.Count);

            foreach (var user in users)
                Serialise(user);
        }

        private void Serialise(MessengerUser user)
        {
            _data.Add(user.AvatarData.Id);
            _data.Add(user.AvatarData.Name);
            _data.Add(user.AvatarData.Motto);
            _data.Add(user.IsOnline);
            _data.Add(false);
            _data.Add(string.Empty);
            _data.Add(0);
            _data.Add(user.AvatarData.Figure);
            _data.Add(user.AvatarData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss"));
        }

        public override int HeaderId => 435;
    }
}