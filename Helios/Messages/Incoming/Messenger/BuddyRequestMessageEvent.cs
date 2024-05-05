using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Messages.Outgoing.Messenger;
using Helios.Network.Streams;
using Helios.Storage.Access;
using Helios.Storage.Models.Messenger;

namespace Helios.Messages.Incoming
{
    class BuddyRequestMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int AvatarId = AvatarDao.GetIdByName(request.ReadString());

            if (AvatarId < 1)
                return;

            var targetMessenger = Messenger.GetMessengerData(AvatarId);
            var targetAvatar = AvatarManager.Instance.GetAvatarById(AvatarId);

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
                AvatarId = AvatarId
            };

            MessengerDao.SaveRequest(messengerRequest);

            targetMessenger.Requests.Add(avatar.Messenger.MessengerUser);

            if (targetAvatar != null)
                targetAvatar.Send(new MessengerRequestComposer(avatar.Details));
        }
    }
}
