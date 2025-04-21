using Helios.Game;
using Helios.Messages.Incoming;
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

            bool isLoading = request.ReadBool();
            bool checkEntry = request.ReadBool();

            avatar.Send(new GetGuestRoomResultComposer(room.Data, isLoading, checkEntry));
        }


        public int HeaderId => 385;
    }
}