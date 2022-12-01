﻿using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    class InitMessengerMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new InitMessengerComposer(
                ValueManager.Instance.GetInt("max.friends.normal"),
                ValueManager.Instance.GetInt("max.friends.hc"),
                ValueManager.Instance.GetInt("max.friends.vip"),
                player.Messenger.Categories, 
                player.Messenger.Friends
            ));

            player.Send(new MessengerRequestsComposer(player.Messenger.Requests));

            var unreadMessages = MessengerDao.GetUneadMessages(player.Details.Id);

            if (unreadMessages.Count > 0)
            {
                foreach (var unreadMessage in unreadMessages)
                    player.Send(new InstantChatComposer(unreadMessage.UserId, unreadMessage.Message));

                MessengerDao.SetReadMessages(player.Details.Id);
            }
        }
    }
}
