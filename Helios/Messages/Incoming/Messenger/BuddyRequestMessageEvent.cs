using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Messages.Outgoing.Messenger;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;
using Helios.Storage.Database.Data;

namespace Helios.Messages.Incoming
{
    class BuddyRequestMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int userId = PlayerDao.GetIdByName(request.ReadString());

            if (userId < 1)
                return;

            var targetMessenger = Messenger.GetMessengerData(userId);
            var targetPlayer = PlayerManager.Instance.GetPlayerById(userId);

            if (targetMessenger == null || 
                targetMessenger.HasFriend(player.Details.Id) || 
                targetMessenger.HasRequest(player.Details.Id))
                return;

            if (!targetMessenger.FriendRequestsEnabled)
            {
                player.Send(new MessengerRequestErrorComposer(MessengerRequestError.FriendRequestsDisabled));
                return;
            }

            if (player.Messenger.Friends.Count >= player.Messenger.MaxFriendsAllowed)
            {
                player.Send(new MessengerRequestErrorComposer(MessengerRequestError.FriendListFull));
                return;
            }

            var messengerRequest = new MessengerRequestData
            {
                FriendId = player.Details.Id,
                UserId = userId
            };

            MessengerDao.SaveRequest(messengerRequest);

            targetMessenger.Requests.Add(player.Messenger.MessengerUser);

            if (targetPlayer != null)
                targetPlayer.Send(new MessengerRequestComposer(player.Details));
        }
    }
}
