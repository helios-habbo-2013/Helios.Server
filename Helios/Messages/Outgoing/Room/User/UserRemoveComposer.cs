using System;

namespace Helios.Messages.Outgoing
{
    public class UserRemoveComposer : IMessageComposer
    {
        private int instanceId;

        public UserRemoveComposer(int instanceId)
        {
            this.instanceId = instanceId;
        }

        public override void Write()
        {
            _data.Add(Convert.ToString(instanceId));
        }
    }
}
