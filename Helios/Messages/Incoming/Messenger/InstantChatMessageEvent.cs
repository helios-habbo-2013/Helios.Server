using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Messenger;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    class InstantChatMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int AvatarId = request.ReadInt();

            try
            {
                if (!avatar.Messenger.HasFriend(AvatarId))
                {
                    avatar.Send(new InstantChatErrorComposer(InstantChatError.NotFriend, AvatarId));
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
                    avatar.Send(new InstantChatErrorComposer(InstantChatError.FriendOffline, AvatarId));
                    return;
                }

                friend.Avatar.Send(new InstantChatComposer(avatar.Details.Id, chatMessage));
            }
            catch
            {
                avatar.Send(new InstantChatErrorComposer(InstantChatError.SendingFailed, AvatarId));
            }
        }

        public int HeaderId => 33;
    }
}
