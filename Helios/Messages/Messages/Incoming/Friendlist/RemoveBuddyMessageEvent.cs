using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class RemoveBuddyMessageEvent : IMessageEvent
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

                using (var context = new StorageContext())
                {
                    context.DeleteRequests(avatar.Details.Id, AvatarId);
                    context.DeleteFriends(avatar.Details.Id, AvatarId);
                }

                messenger.QueueUpdate(MessengerUpdateType.RemoveFriend, new MessengerUser(avatarData));
            }

            messenger.ForceUpdate();
        }

        public int HeaderId => 40;
    }
}