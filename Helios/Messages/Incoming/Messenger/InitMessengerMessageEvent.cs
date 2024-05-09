using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

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

            using (var context = new GameStorageContext())
            {
                var unreadMessages = context.GetUneadMessages(avatar.Details.Id);

                if (unreadMessages.Count > 0)
                {
                    foreach (var unreadMessage in unreadMessages)
                        avatar.Send(new InstantChatComposer(unreadMessage.AvatarId, unreadMessage.Message));

                    context.SetReadMessages(avatar.Details.Id);
                }
            }
        }
    }
}
