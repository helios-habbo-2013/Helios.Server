using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Network.Streams;
using Helios.Storage.Models.Messenger;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class AcceptBuddyMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int friendsAccepted = request.ReadInt();
            var messenger = avatar.Messenger;

            for (int i = 0; i < friendsAccepted; i++)
            {
                int AvatarId = request.ReadInt();

                if (!messenger.HasRequest(AvatarId))
                    continue;

                if (messenger.Friends.Count >= messenger.MaxFriendsAllowed)
                    continue;

                var avatarData = AvatarManager.Instance.GetDataById(AvatarId);

                if (avatarData == null)
                    continue;

                var targetMessenger = Messenger.GetMessengerData(AvatarId);
                var targetFriend = new MessengerUser(avatarData);

                targetMessenger.Friends.Add(messenger.MessengerUser);
                messenger.Friends.Add(targetFriend);

                targetMessenger.RemoveRequest(avatar.Details.Id);
                messenger.RemoveRequest(AvatarId);

                var targetAvatar = AvatarManager.Instance.GetAvatarById(AvatarId);

                if (targetAvatar != null)
                {
                    targetAvatar.Messenger.QueueUpdate(MessengerUpdateType.AddFriend, messenger.MessengerUser);
                    targetAvatar.Messenger.ForceUpdate();
                }

                using (var context = new StorageContext())
                {
                    context.DeleteRequests(avatar.Details.Id, AvatarId);
                    context.SaveFriend(new MessengerFriendData
                    {
                        FriendId = AvatarId,
                        AvatarId = avatar.Details.Id
                    });
                    context.SaveFriend(new MessengerFriendData
                    {
                        AvatarId = AvatarId,
                        FriendId = avatar.Details.Id
                    });
                }

                messenger.QueueUpdate(MessengerUpdateType.AddFriend, targetFriend);
            }

            messenger.ForceUpdate();
        }

        public int HeaderId => 37;
    }
}