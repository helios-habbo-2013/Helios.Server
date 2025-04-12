using System.Collections.Generic;
using System.Linq;
using Helios.Game;
using Helios.Storage.Models.Avatar;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Room;

namespace Helios.Messages.Outgoing
{
    class GroupInfoMessageComposer : IMessageComposer
    {
        private Group group;
        private AvatarData avatarData;
        private RoomData roomData;
        private bool newWindow;

        public GroupInfoMessageComposer(Group group, AvatarData avatarData, RoomData roomData, bool newWindow)
        {
            this.group = group;
            this.avatarData = avatarData;
            this.roomData = roomData;
            this.newWindow = newWindow;
        }

        public override void Write()
        {
            _data.Add(group.Data.Id);
            _data.Add(true);
            _data.Add((int) group.Data.GroupType);
            _data.Add(group.Data.Name);
            _data.Add(group.Data.Description);
            _data.Add(group.Data.Badge);
            _data.Add(roomData != null ? roomData.Id : 0);
            _data.Add(roomData != null ? roomData.Name : "");
            _data.Add((int) group.GetMemberType(avatarData.Id)); // TODO: Membership type, 1 = is member, 2 is pending, 0 = none
            _data.Add(group.Members.Count - group.Members.Count(x => x.Data.MemberType == Storage.Models.Group.GroupMembershipType.PENDING)); // TODO: Membership
            _data.Add(false);
            _data.Add(group.Data.CreatedAt.ToString("dd-MM-yyyy"));
            _data.Add(group.Data.OwnerId == avatarData.Id); // TODO: Is owner
            _data.Add(group.GetMemberType(avatarData.Id) == GroupMembershipType.ADMIN);
            _data.Add(group.Data.OwnerData.Name);
            _data.Add(newWindow);
            _data.Add(group.Data.AllowMembersDecorate); // TODO: Can members decorate
            _data.Add((group.GetMemberType(avatarData.Id) == GroupMembershipType.ADMIN || group.Data.OwnerId == avatarData.Id) ? group.Members.Count(x => x.Data.MemberType == GroupMembershipType.PENDING) : 0); // TODO: See memebrship request count if owner or admin
        }

        public override int HeaderId => -1;
    }
}
