using Helios.Game;
using Helios.Messages.Outoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class GetRoomSettingsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            Room room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null || !room.RightsManager.IsOwner(avatar.Details.Id))
                return;

            avatar.Send(new RoomSettingsDataComposer(room));
        }
    }
}
