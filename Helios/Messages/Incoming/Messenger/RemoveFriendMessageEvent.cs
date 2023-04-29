using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    class RemoveFriendMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int friendsToDelete = request.ReadInt();
            var messenger = avatar.Messenger;

            for (int i = 0; i < friendsToDelete; i++)
            {
                int AvatarId = request.ReadInt();

                if (!messenger.HasFriend(AvatarId))
                    continue;

                if (messenger.Friends.Count >= messenger.MaxFriendsAllowed)
                    continue;

                var avatarData = AvatarManager.Instance.GetDataById(AvatarId);

                if (avatarData == null)
                    continue;

                var targetMessenger = Messenger.GetMessengerData(AvatarId);

                targetMessenger.RemoveFriend(avatar.Details.Id);
                messenger.RemoveFriend(AvatarId);

                var targetAvatar = AvatarManager.Instance.GetAvatarById(AvatarId);

                if (targetAvatar != null)
                {
                    targetAvatar.Messenger.QueueUpdate(MessengerUpdateType.RemoveFriend, messenger.MessengerUser);
                    targetAvatar.Messenger.ForceUpdate();
                }

                MessengerDao.DeleteRequests(avatar.Details.Id, AvatarId);
                MessengerDao.DeleteFriends(avatar.Details.Id, AvatarId);

                messenger.QueueUpdate(MessengerUpdateType.RemoveFriend, new MessengerUser(avatarData));
            }

            messenger.ForceUpdate();
        }
    }
}
