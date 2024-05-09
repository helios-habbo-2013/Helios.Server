using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using MySql.Data.MySqlClient.Memcached;

namespace Helios.Messages.Incoming
{
    public class GetGroupInfoMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();
            bool flag = request.ReadBoolean();

            using (var context = new GameStorageContext())
            {
                var groupData = context.GetGroup(groupId);

                if (groupData == null)
                {
                    return;
                }

                avatar.Send(new GroupInfoMessageComposer(groupData, groupData.RoomData, flag, groupData.OwnerId == avatar.Details.Id));
            }
        }
    }
}
