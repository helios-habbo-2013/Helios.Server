using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class GetGroupInfoMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();
            bool flag = request.ReadBoolean();


            var groupData = GroupManager.Instance.GetGroup(groupId);

            if (groupData == null)
            {
                return;
            }

            avatar.Send(new GroupInfoMessageComposer(groupData, avatar.Details, groupData.Data.RoomData, flag));
        }

    }
}
