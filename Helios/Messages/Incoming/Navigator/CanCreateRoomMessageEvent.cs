using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class CanCreateRoomMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed");

            if (player.IsSubscribed)
                maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed.subscribed");

            player.Send(new CanCreateRoomComposer(
                maxRoomsAllowed >= RoomDao.CountUserRooms(player.Details.Id),
                maxRoomsAllowed)
            );
        }
    }
}
