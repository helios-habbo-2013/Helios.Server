using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class FollowFriendMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int friendId = request.ReadInt();

            if (!avatar.Messenger.HasFriend(friendId))
            {
                avatar.Send(new FollowFriendFailedComposer(FollowBuddyError.NotFriend));
                return;
            }

            var friend = avatar.Messenger.GetFriend(friendId);

            if (!friend.IsOnline)
            {
                avatar.Send(new FollowFriendFailedComposer(FollowBuddyError.Offline));
                return;
            }

            if (!friend.InRoom)
            {
                avatar.Send(new FollowFriendFailedComposer(FollowBuddyError.HotelView));
                return;
            }

            Room room = friend.Avatar.RoomUser.Room;
            avatar.Send(new RoomForwardMessageComposer(room.Data.Id, room.Data.IsPublicRoom));
        }

        public int HeaderId => -1;
    }
}