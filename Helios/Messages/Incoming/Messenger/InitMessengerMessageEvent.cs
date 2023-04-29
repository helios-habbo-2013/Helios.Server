using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    class InitMessengerMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new InitMessengerComposer(
                ValueManager.Instance.GetInt("max.friends.normal"),
                ValueManager.Instance.GetInt("max.friends.hc"),
                ValueManager.Instance.GetInt("max.friends.vip"),
                avatar.Messenger.Categories, 
                avatar.Messenger.Friends
            ));

            avatar.Send(new MessengerRequestsComposer(avatar.Messenger.Requests));

            var unreadMessages = MessengerDao.GetUneadMessages(avatar.Details.Id);

            if (unreadMessages.Count > 0)
            {
                foreach (var unreadMessage in unreadMessages)
                    avatar.Send(new InstantChatComposer(unreadMessage.AvatarId, unreadMessage.Message));

                MessengerDao.SetReadMessages(avatar.Details.Id);
            }
        }
    }
}
