using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    public class CanCreateRoomMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed");

            if (avatar.IsSubscribed)
                maxRoomsAllowed = ValueManager.Instance.GetInt("max.rooms.allowed.subscribed");

            using (var context = new StorageContext())
            {
                avatar.Send(new CanCreateRoomComposer(
                    maxRoomsAllowed >= context.CountUserRooms(avatar.Details.Id),
                    maxRoomsAllowed)
                );
            }
        }

        public int HeaderId => -1;
    }
}
