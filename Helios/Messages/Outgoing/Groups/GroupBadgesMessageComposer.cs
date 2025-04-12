using System.Collections.Generic;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class GroupBadgesMessageComposer : IMessageComposer
    {
        private int groupId;
        private string badge;

        public GroupBadgesMessageComposer(int id, string badge)
        {
            this.groupId = id;
            this.badge = badge;
        }

        public override void Write()
        {
            _data.Add(1); // size: loop
            _data.Add(this.groupId);
            _data.Add(this.badge);
        }

        public int HeaderId => -1;
    }
}
