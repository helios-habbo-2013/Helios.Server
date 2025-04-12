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
                _data.Add(group.ColourA);
                _data.Add(group.ColourB);
                _data.Add(false); // Whether group is favourite
            }
        }

        public virtual int HeaderId => 0;
    }
}
