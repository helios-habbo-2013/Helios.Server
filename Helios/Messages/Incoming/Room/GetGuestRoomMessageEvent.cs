using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetGuestRoomMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null)
                return;

            bool isLoading = request.ReadIntAsBool();
            bool checkEntry = request.ReadIntAsBool();

            avatar.Send(new RoomInfoComposer(room.Data, isLoading, checkEntry));
        }
    }
}
