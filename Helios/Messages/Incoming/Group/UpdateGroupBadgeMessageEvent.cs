using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Access;
using System.Text;

namespace Helios.Messages.Incoming
{
    public class UpdateGroupBadgeMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || group.Data.OwnerId != avatar.EntityData.Id)
            {
                return;
            }

            int badgeElements = request.ReadInt() / 3;

            StringBuilder badgeBuilder = new StringBuilder();

            for (int i = 0; i < badgeElements; i++)
            {
                if (i > 0)
                {
                    badgeBuilder.Append("s");
                }
                else
                {
                    badgeBuilder.Append("b");
                }

                int badgeElementId = request.ReadInt();
                int badgeElementColour = request.ReadInt();
                int badgeElementPosition = request.ReadInt();

                badgeBuilder.Append(badgeElementId.ToString().PadLeft(3, '0'));
                badgeBuilder.Append(badgeElementColour.ToString());

                if (i > 0)
                {
                    badgeBuilder.Append(badgeElementPosition);
                }
                //else
                //{
                //    badgeBuilder.Append("X");
                //}
            }

            string badgeCode = badgeBuilder.ToString();

            group.Data.Badge = badgeCode;

            using (var context = new GameStorageContext())
            {
                context.UpdateGroup(group.Data);
            }

            var room = RoomManager.Instance.GetRoom(group.Data.RoomId);

            if (room != null && room.IsActive)
            {
                room.Send(new GroupBadgesMessageComposer(group.Data.Id, group.Data.Badge));
                room.Send(new RoomInfoComposer(room.Data));
            }

            avatar.Send(new GroupInfoMessageComposer(group, avatar.Details, group.Data.RoomData, false));
        }
    }
}
