using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Messages.Outgoing.Messenger;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Messenger;

namespace Helios.Messages.Incoming
{
    class BuddyRequestMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int avatarId;

            using (var context = new StorageContext())
            {
                avatarId = context.GetIdByName(request.ReadString());
            }

            if (avatarId < 1)
                return;

            var targetMessenger = Messenger.GetMessengerData(avatarId);
            var targetAvatar = AvatarManager.Instance.GetAvatarById(avatarId);

            if (targetMessenger == null || 
                targetMessenger.HasFriend(avatar.Details.Id) || 
                targetMessenger.HasRequest(avatar.Details.Id))
                return;

            if (!targetMessenger.FriendRequestsEnabled)
            {
                avatar.Send(new MessengerRequestErrorComposer(MessengerRequestError.FriendRequestsDisabled));
                return;
            }

            if (avatar.Messenger.Friends.Count >= avatar.Messenger.MaxFriendsAllowed)
            {
                avatar.Send(new MessengerRequestErrorComposer(MessengerRequestError.FriendListFull));
                return;
            }

            var messengerRequest = new MessengerRequestData
            {
                FriendId = avatar.Details.Id,
                AvatarId = avatarId
            };

            using (var context = new StorageContext())
            {
                context.SaveRequest(messengerRequest);
            }

            targetMessenger.Requests.Add(avatar.Messenger.MessengerUser);

            if (targetAvatar != null)
                targetAvatar.Send(new MessengerRequestComposer(avatar.Details));
        }
    }
}
