using System.Collections.Generic;
using Helios.Game;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Room;

namespace Helios.Messages.Outgoing
{
    class GroupInfoMessageComposer : IMessageComposer
    {
        private GroupData groupData;
        private RoomData roomData;
        private bool flag;
        private bool isOwner;

        public GroupInfoMessageComposer(GroupData groupData, RoomData roomData, bool flag, bool isOwner)
        {
            this.groupData = groupData;
            this.roomData = roomData;
            this.flag = flag;
            this.isOwner = isOwner;
        }

        public override void Write()
        {
            _data.Add(groupData.Id);
            _data.Add(true);
            _data.Add((int) groupData.GroupType);
            _data.Add(groupData.Name);
            _data.Add(groupData.Description);
            _data.Add(groupData.Badge);
            _data.Add(roomData != null ? roomData.Id : 0);
            _data.Add(roomData != null ? roomData.Name : "");
            _data.Add(0); // TODO: Membership type, 1 = is member, 2 is pending, 0 = none
            _data.Add(0); // TODO: Membership
            _data.Add(false);
            _data.Add(groupData.CreatedAt.ToString("dd-MM-yyyy"));
            _data.Add(isOwner); // TODO: Is owner
            _data.Add(false);
            _data.Add(groupData.OwnerData.Name);
            _data.Add(flag);
            _data.Add(false); // TODO: Can members decorate
            _data.Add(0); // TODO: See memebrship request count if owner or admin
            /*
            msg.writeBoolean(true); //is visible
            msg.writeInt(group.getData().getType().getTypeId());
            msg.writeString(group.getData().getTitle());
            msg.writeString(group.getData().getDescription());
            msg.writeString(group.getData().getBadge());
            msg.writeInt(roomData == null ? 0 : roomData.getId());
            msg.writeString(roomData == null ? "Unknown Room" : roomData.getName());
            msg.writeInt(membership);
            msg.writeInt(group.getMembers().getAll().size());
            msg.writeBoolean(false);
            msg.writeString(getDate(group.getData().getCreatedTimestamp()));
            msg.writeBoolean(isOwner);
            msg.writeBoolean(isAdmin);

            msg.writeString(group.getData().getOwnerName());

            msg.writeBoolean(flag);
            msg.writeBoolean(group.getData().canMembersDecorate());

            msg.writeInt((isOwner || isAdmin) ? group.getMembers().getMembershipRequests().size() : 0);
            msg.writeBoolean(group.getData().hasForum());
            */
        }
    }
}
