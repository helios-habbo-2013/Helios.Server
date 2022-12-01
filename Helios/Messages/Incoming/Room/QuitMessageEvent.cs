using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class QuitMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            player.RoomUser.AuthenticateRoomId = -1;
            player.RoomUser.AuthenticateTeleporterId = null;

            player.RoomUser.Room.EntityManager.LeaveRoom(player, true);
        }
    }
}
