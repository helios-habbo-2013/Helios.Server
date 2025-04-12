using Helios.Game;
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
                avatar.Send(new FollowBuddyErrorComposer(FollowBuddyError.NotFriend));
                return;
            }

            var friend = avatar.Messenger.GetFriend(friendId);

            if (!friend.IsOnline)
            {
                avatar.Send(new FollowBuddyErrorComposer(FollowBuddyError.Offline));
                return;
            }

            if (!friend.InRoom)
            {
                avatar.Send(new FollowBuddyErrorComposer(FollowBuddyError.HotelView));
                return;
            }

            Room room = friend.Avatar.RoomUser.Room;
            avatar.Send(new RoomForwardComposer(room.Data.Id, room.Data.IsPublicRoom));
        }

        public int HeaderId => -1;
    }
}
