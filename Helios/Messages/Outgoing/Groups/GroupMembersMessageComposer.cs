using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Helios.Game;
using Helios.Storage.Models.Group;

namespace Helios.Messages.Outgoing
{
    class GroupMembersMessageComposer : IMessageComposer
    {
        private static readonly int MEMBERS_PER_PAGE = 14;

        private Group group;
        private int page;
        private List<GroupMembership> members;
        private int requestType;
        private string searchQuery;
        private bool isAdmin;

        public GroupMembersMessageComposer(Group group, int page, List<GroupMembership> members, int requestType, string searchQuery, bool isAdmin)
        {
            this.group = group;
            this.page = page;
            this.members = members;
            this.requestType = requestType;
            this.searchQuery = searchQuery;
            this.isAdmin = isAdmin;
        }

        public override void Write()
        {
            _data.Add(this.group.Data.Id);
            _data.Add(this.group.Data.Name);
            _data.Add(this.group.Data.RoomId);
            _data.Add(this.group.Data.Badge);
            _data.Add(this.members.Count);

            var paginatedMembers = this.members.Batch(MEMBERS_PER_PAGE);

            if (!paginatedMembers.Any())
            {
                _data.Add(0);
            }
            else
            {
                if (page > paginatedMembers.Count - 1)
                {
                    page = paginatedMembers.Count - 1;
                }

                if (page < 0)
                {
                    page = 0;
                }

                var memberList = paginatedMembers[page];

                _data.Add(memberList.Count);

                foreach (var member in memberList)
                {
                    if (group.IsAdmin(member.Data.AvatarId))
                    {
                        _data.Add(group.Data.OwnerId == member.Data.AvatarId ? 0 : 1);
                    }
                    else
                    {
                        if (requestType == 2)
                        {
                            _data.Add(3);
                        }
                        else
                        {
                            _data.Add(2);
                        }
                    }

                    _data.Add(member.Data.AvatarId);
                    _data.Add(member.Data.Avatar.Name);
                    _data.Add(member.Data.Avatar.Figure);
                    _data.Add(member.Data.CreatedAt.ToShortDateString());

                }
            }


            _data.Add(this.isAdmin);
            _data.Add(MEMBERS_PER_PAGE);
            _data.Add(this.page);
            _data.Add(this.requestType);
            _data.Add(this.searchQuery);
        }

        public int HeaderId => -1;
    }
}
