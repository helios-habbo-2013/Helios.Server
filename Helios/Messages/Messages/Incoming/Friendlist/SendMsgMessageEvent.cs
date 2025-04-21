using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Network.Streams;
using Helios.Storage.Models.Messenger;
using Helios.Storage;
using Helios.Util.Extensions;
using Helios.Storage.Access;
using Helios.Messages.Outgoing;

namespace Helios.Messages.Incoming
{
    class SendMsgMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int AvatarId = request.ReadInt();

            try
            {
                if (!avatar.Messenger.HasFriend(AvatarId))
                {
                    avatar.Send(new InstantMessageErrorComposer(InstantChatError.NotFriend, AvatarId));
                    return;
                }

                var friend = avatar.Messenger.GetFriend(AvatarId);
                var chatMessage = request.ReadString().FilterInput(false);

                var chatMessageData = new MessengerChatData
                {
                    AvatarId = avatar.Details.Id,
                    FriendId = AvatarId,
                    Message = chatMessage,
                    IsRead = friend.IsOnline
                };

                using (var context = new StorageContext())
                {
                    context.SaveMessage(chatMessageData);
                }

                if (!friend.IsOnline)
                {
                    avatar.Send(new InstantMessageErrorComposer(InstantChatError.FriendOffline, AvatarId));
                    return;
                }

                friend.Avatar.Send(new NewConsoleMessageComposer(avatar.Details.Id, chatMessage));
            }
            catch
            {
                avatar.Send(new InstantMessageErrorComposer(InstantChatError.SendingFailed, AvatarId));
            }
        }

        public int HeaderId => 33;
    }
}