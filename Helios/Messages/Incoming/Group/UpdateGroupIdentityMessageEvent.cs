using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using MySql.Data.MySqlClient.Memcached;

namespace Helios.Messages.Incoming
{
    public class UpdateGroupIdentityMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || group.Data.OwnerId != avatar.EntityData.Id)
            {
                return;
            }

            string name = request.ReadString();
            string description = request.ReadString();

            group.Data.Name = name;
            group.Data.Description = description;

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
