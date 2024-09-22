using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using MySql.Data.MySqlClient.Memcached;
using System.Text;

namespace Helios.Messages.Incoming
{
    public class UpdateGroupColoursMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || group.Data.OwnerId != avatar.EntityData.Id)
            {
                return;
            }

            int colour1 = request.ReadInt();
            int colour2 = request.ReadInt();

            group.Data.Colour1 = colour1;
            group.Data.Colour2 = colour2;

            using (var context = new GameStorageContext())
            {
                context.SaveGroup(group.Data);
            }

            var room = RoomManager.Instance.GetRoom(group.Data.RoomId);

            if (room != null && room.IsActive)
            {
                room.Send(new RoomInfoComposer(room.Data));
            }
        }
    }
}
