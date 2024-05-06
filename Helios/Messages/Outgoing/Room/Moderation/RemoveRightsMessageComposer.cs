using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Messages.Outgoing
{
    public class RemoveRightsMessageComposer : IMessageComposer
    {
        private int roomId;
        private int avatarId;

        public RemoveRightsMessageComposer(int roomId, int avatarId)
        {
            this.roomId = roomId;
            this.avatarId = avatarId;
        }

        public override void Write()
        {
            _data.Add(this.roomId);
            _data.Add(this.avatarId);
        }
    }
}
