using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class OpenFlatConnectionMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int roomId = request.ReadInt();
            string password = request.ReadString();

            // TODO: Passworded bullshit
            // TODO: Full room bullshit
            // TODO: Door knocking bullshit

            var room = RoomManager.Instance.GetRoom(roomId);

            if (room != null)
            {
                player.Send(new OpenConnectionComposer(roomId, room.Data.CategoryId));
            }
        }
    }
}
