using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    class ToggleRoomMuteMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {     
            var room = player.RoomUser.Room;
            
            if (room == null)
                return;


            room.Data.IsMuted = !room.Data.IsMuted;

            player.Send(new RoomMuteSettingsComposer(room.Data.IsMuted));
            RoomDao.SaveRoom(room.Data);
        }
    }
}
