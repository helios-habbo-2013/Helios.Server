using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetPublicRoomMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null)
                return;

            avatar.Send(new PublicRoomCastsComposer(room.Data.Id, room.Data.CCTs));
        }
    }
}
