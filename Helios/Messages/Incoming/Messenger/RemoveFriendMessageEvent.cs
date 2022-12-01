﻿using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    class RemoveFriendMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int friendsToDelete = request.ReadInt();
            var messenger = player.Messenger;

            for (int i = 0; i < friendsToDelete; i++)
            {
                int userId = request.ReadInt();

                if (!messenger.HasFriend(userId))
                    continue;

                if (messenger.Friends.Count >= messenger.MaxFriendsAllowed)
                    continue;

                var playerData = PlayerManager.Instance.GetDataById(userId);

                if (playerData == null)
                    continue;

                var targetMessenger = Messenger.GetMessengerData(userId);

                targetMessenger.RemoveFriend(player.Details.Id);
                messenger.RemoveFriend(userId);

                var targetPlayer = PlayerManager.Instance.GetPlayerById(userId);

                if (targetPlayer != null)
                {
                    targetPlayer.Messenger.QueueUpdate(MessengerUpdateType.RemoveFriend, messenger.MessengerUser);
                    targetPlayer.Messenger.ForceUpdate();
                }

                MessengerDao.DeleteRequests(player.Details.Id, userId);
                MessengerDao.DeleteFriends(player.Details.Id, userId);

                messenger.QueueUpdate(MessengerUpdateType.RemoveFriend, new MessengerUser(playerData));
            }

            messenger.ForceUpdate();
        }
    }
}
