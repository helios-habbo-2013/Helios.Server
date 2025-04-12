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
    public class GiveGroupAdminMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();
            int playerId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || !(group.Data.OwnerId == avatar.Details.Id))
            {
                return;
            }

            var groupMembership = group.Members.FirstOrDefault(x =>
                x.Data.AvatarId == playerId &&
                x.Data.MemberType == GroupMembershipType.MEMBER
            );

            if (groupMembership == null)
            {
                return;
            }

            groupMembership.Data.MemberType = GroupMembershipType.ADMIN;

            using (var context = new StorageContext())
            {
                context.UpdateMembership(groupMembership.Data);
            }

            int requestType = (int) avatar.LocalStorage["groupMemberSearch_requestType"];

            List<GroupMembership> avatars = requestType switch
            {
                1 => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.ADMIN).ToList(),
                2 => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.PENDING).ToList(),
                _ => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.MEMBER).ToList(),
            };

            avatar.Send(new GroupMembersMessageComposer(group, 
                (int) avatar.LocalStorage["groupMemberSearch_page"], avatars,
                (int) avatar.LocalStorage["groupMemberSearch_requestType"],
                (string) avatar.LocalStorage["groupMemberSearch_searchQuery"],
                group.IsAdmin(avatar.Details.Id)));
        }

        public int HeaderId => -1;
    }
}
