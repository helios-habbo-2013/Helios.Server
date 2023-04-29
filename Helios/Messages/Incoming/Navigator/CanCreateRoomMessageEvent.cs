using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class CanCreateRoomMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed");

            if (avatar.IsSubscribed)
                maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed.subscribed");

            avatar.Send(new CanCreateRoomComposer(
                maxRoomsAllowed >= RoomDao.CountUserRooms(avatar.Details.Id),
                maxRoomsAllowed)
            );
        }
    }
}
