using Helios.Game;
using System;

namespace Helios.Messages.Outgoing
{
    class UserRemoveMessageComposer : IMessageComposer
    {
        private int _instanceId;

        public UserRemoveMessageComposer(int instanceId)
        {
            this._instanceId = instanceId;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(Convert.ToString(this._instanceId));
        }


        public override int HeaderId => 29;
    }
}