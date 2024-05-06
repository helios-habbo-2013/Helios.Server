using System.Collections.Generic;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class GroupBadgesMessageComposer : IMessageComposer
    {
        private int? groupId;
        private string groupName;

        public GroupBadgesMessageComposer(int id, string name)
        {
            this.groupId = id;
            this.groupName = name;
        }

        public override void Write()
        {
            if (this.groupId != null)
            {
                _data.Add(1);
                _data.Add(this.groupId);
                _data.Add(this.groupName);
            }
        }
    }
}
