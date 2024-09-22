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
    public class UpdateGroupSettingsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || group.Data.OwnerId != avatar.EntityData.Id)
            {
                return;
            }

            int groupType = request.ReadInt();
            int rightsType = request.ReadInt();

            if (groupType >= 0 && groupType <= 2)
            {
                group.Data.GroupType = (GroupType) groupType;
            }

            if (rightsType >= 0 && rightsType <= 2)
            {
                group.Data.AllowMembersDecorate = rightsType == 0;
            }

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
