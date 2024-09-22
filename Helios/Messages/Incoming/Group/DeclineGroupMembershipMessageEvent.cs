using DotNetty.Common.Utilities;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using MySql.Data.MySqlClient.Memcached;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Incoming
{
    public class DeclineGroupMembershipMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();
            int playerId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || !group.IsAdmin(avatar.Details.Id))
            {
                return;
            }

            var groupMembership = group.Members.FirstOrDefault(x =>
                x.Data.AvatarId == playerId &&
                x.Data.MemberType == GroupMembershipType.PENDING
            );

            if (groupMembership == null)
            {
                return;
            }

            using (var context = new GameStorageContext())
            {
                context.DeleteMembership(groupMembership.Data);
            }

            group.Members.Remove(groupMembership);

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

            var player = AvatarManager.Instance.GetAvatarById(playerId);

            if (player != null)
            {
                avatar.Send(new GroupInfoMessageComposer(group, avatar.Details, group.Data.RoomData, false));
            }
        }
    }
}
