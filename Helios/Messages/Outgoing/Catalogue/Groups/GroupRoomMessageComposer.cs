using System;
using System.Collections.Generic;
using Helios.Game;
using Helios.Storage.Models.Group;

namespace Helios.Messages.Outgoing
{
    class GroupRoomMessageComposer : IMessageComposer
    {
        private int roomId;
        private int groupId;

        public GroupRoomMessageComposer(int roomId, int id)
        {
            this.roomId = roomId;
            this.groupId = id;
        }

        public override void Write()
        {
            _data.Add(this.roomId);
            _data.Add(this.groupId);
        }

        public int HeaderId => -1;
    }
}
