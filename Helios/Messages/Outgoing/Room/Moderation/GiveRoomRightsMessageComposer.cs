using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Messages.Outgoing
{
    public class GiveRoomRightsMessageComposer : IMessageComposer
    {
        private int roomId;
        private int avatarId;
        private string avatarName;

        public GiveRoomRightsMessageComposer(int roomId, int avatarId, string avatarName)
        {
            this.roomId = roomId;
            this.avatarId = avatarId;
            this.avatarName = avatarName;
        }

        public override void Write()
        {
            _data.Add(this.roomId);
            _data.Add(this.avatarId);
            _data.Add(this.avatarName);
        }
    }
}
