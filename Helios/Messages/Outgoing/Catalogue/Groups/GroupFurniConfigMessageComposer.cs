using System;
using System.Collections.Generic;
using Helios.Game;
using Helios.Storage.Models.Group;

namespace Helios.Messages.Outgoing
{
    class GroupFurniConfigMessageComposer : IMessageComposer
    {
        private int avatarId;
        private List<Group> groupList;

        public GroupFurniConfigMessageComposer(int avatarId, List<Group> groupList)
        {
            this.avatarId = avatarId;
            this.groupList = groupList;
        }

        public override void Write()
        {
            _data.Add(this.groupList.Count);

            foreach (var group in groupList)
            {
                _data.Add(group.Data.Id);
                _data.Add(group.Data.Name);
                _data.Add(group.Data.Badge);

                var colour1 = GroupManager.Instance.BadgeManager.Colour2[group.Data.Colour1].FirstValue;
                var colour2 = GroupManager.Instance.BadgeManager.Colour3[group.Data.Colour2].FirstValue;

                _data.Add(colour1);
                _data.Add(colour2);

                _data.Add(this.avatarId == group.Data.OwnerId);
                _data.Add(group.Data.OwnerId);
            }
        }
    }
}
