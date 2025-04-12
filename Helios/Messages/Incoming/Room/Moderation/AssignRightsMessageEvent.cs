using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using System.Linq;

namespace Helios.Messages.Incoming
{
    class AssignRightsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = avatar.RoomUser.Room;

            if (room == null || !room.RightsManager.HasRights(avatar.Details.Id))
            {
                return;
            }

            int playerId = request.ReadInt();

            room.RightsManager.AddRights(playerId);

            avatar.Send(new GiveRoomRightsMessageComposer(room.Data.Id, playerId, AvatarManager.Instance.GetDataById(playerId).Name));
        }

        public int HeaderId => -1;
    }
}
