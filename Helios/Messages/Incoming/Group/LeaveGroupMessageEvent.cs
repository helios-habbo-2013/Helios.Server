using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Incoming
{
    public class LeaveGroupMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();
            int avatarId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null && !(group.IsAdmin(avatar.Details.Id)
                || (avatar.Details.Id == avatarId && group.IsMemberType(avatarId, GroupMembershipType.MEMBER))))
            {
                return;
            }

            var groupMembership = group.Members.FirstOrDefault(x =>
                x.Data.AvatarId == avatarId
            );

            using (var context = new StorageContext())
            {
                if (groupMembership != null)
                {
                    context.DeleteMembership(groupMembership.Data);
                }

                group.RefreshMembers();

                var player = AvatarManager.Instance.GetAvatarById(avatarId);

                if (player != null)
                {
                    avatar.Send(new GroupInfoMessageComposer(group, avatar.Details, group.Data.RoomData, false));
                }
            }

            if (avatar.LocalStorage.ContainsKey("groupMemberSearch_requestType") &&
                avatar.LocalStorage.ContainsKey("groupMemberSearch_page") &&
                avatar.LocalStorage.ContainsKey("groupMemberSearch_searchQuery"))
            {
                int requestType = (int)avatar.LocalStorage["groupMemberSearch_requestType"];

                List<GroupMembership> avatars = requestType switch
                {
                    1 => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.ADMIN).ToList(),
                    2 => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.PENDING).ToList(),
                    _ => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.MEMBER).ToList(),
                };

                avatar.Send(new GroupMembersMessageComposer(group,
                    (int)avatar.LocalStorage["groupMemberSearch_page"], avatars,
                    (int)avatar.LocalStorage["groupMemberSearch_requestType"],
                    (string)avatar.LocalStorage["groupMemberSearch_searchQuery"],
                    group.IsAdmin(avatar.Details.Id)));
            }
        }
    }
}
