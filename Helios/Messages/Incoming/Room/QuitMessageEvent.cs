using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class QuitMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            avatar.RoomUser.AuthenticateRoomId = null;
            avatar.RoomUser.AuthenticateTeleporterId = null;

            avatar.RoomUser.Room.EntityManager.LeaveRoom(avatar, true);
        }

        public int HeaderId => -1;
    }
}
