using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class ToggleRoomMuteMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = avatar.RoomUser.Room;

            if (room == null)
                return;


            room.Data.IsMuted = !room.Data.IsMuted;

            avatar.Send(new RoomMuteSettingsComposer(room.Data.IsMuted));

            using (var context = new StorageContext())
            {
                context.SaveRoom(room.Data);
            }
        }
    }
}
