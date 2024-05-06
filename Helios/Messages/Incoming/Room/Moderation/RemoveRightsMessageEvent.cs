using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class RemoveRightsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = avatar.RoomUser.Room;

            if (room == null || !room.RightsManager.HasRights(avatar.Details.Id))
            {
                return;
            }

            int playerCount = request.ReadInt();

            for (int i = 0; i < playerCount; i++)
            {
                int playerId = request.ReadInt();

                room.RightsManager.RemoveRights(playerId);

                avatar.Send(new RemoveRightsMessageComposer(room.Data.Id, playerId));
            }
        }
    }
}
