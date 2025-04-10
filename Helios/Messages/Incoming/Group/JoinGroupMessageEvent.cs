using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using System.Linq;

namespace Helios.Messages.Incoming
{
    public class JoinGroupMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || group.Data.GroupType == GroupType.PRIVATE)
            {
                return;
            }

            var groupMembership = group.Members.FirstOrDefault(x =>
                x.Data.AvatarId == avatar.Details.Id &&
                x.Data.MemberType == GroupMembershipType.PENDING
            );

            using (var context = new StorageContext())
            {
                if (groupMembership != null)
                {
                    context.DeleteMembership(groupMembership.Data);
                }

                if (avatar.Details.FavouriteGroupId == 0)
                {
                    avatar.Details.FavouriteGroupId = groupId;
                    avatar.Send(new GroupBadgesMessageComposer(group.Data.Id, group.Data.Badge));

                    context.UpdateAvatar(avatar.Details);
                }

                groupMembership = new GroupMembership(new GroupMembershipData
                {
                    AvatarId = avatar.Details.Id,
                    GroupId = groupId,
                    MemberType = group.Data.GroupType == GroupType.LOCKED ? GroupMembershipType.PENDING : GroupMembershipType.MEMBER,
                });

                context.AddMembership(groupMembership.Data);

                group.RefreshMembers();
                
                avatar.Send(new GroupInfoMessageComposer(group, avatar.Details, group.Data.RoomData, false));
            }
        }
    }
}
