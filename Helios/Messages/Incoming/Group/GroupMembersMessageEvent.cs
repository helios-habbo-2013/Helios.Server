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
    public class GroupMembersMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null)
            {
                return;
            }

            int page = request.ReadInt();
            string searchQuery = request.ReadString();
            int requestType = request.ReadInt();

            List<GroupMembership> avatars = requestType switch
            {
                1 => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.ADMIN).ToList(),
                2 => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.PENDING).ToList(),
                _ => group.Members.Where(x => x.Data.MemberType == GroupMembershipType.MEMBER).ToList(),
            };

            if (!string.IsNullOrEmpty(searchQuery))
            {
                foreach (var member in avatars)
                {
                    if (!member.Data.Avatar.Name.ToLower().StartsWith(searchQuery.ToLower()))
                    {
                        avatars.Remove(member);
                    }
                }
            }

            avatar.LocalStorage["groupMemberSearch_page"] = page;
            avatar.LocalStorage["groupMemberSearch_requestType"] = requestType;
            avatar.LocalStorage["groupMemberSearch_searchQuery"] = searchQuery;

            avatar.Send(new GroupMembersMessageComposer(group, page, avatars, requestType, 
                searchQuery,
                group.IsAdmin(avatar.Details.Id)));
        }
    }
}
