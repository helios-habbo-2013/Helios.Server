using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class UserRightsMessageComposer : IMessageComposer
    {
        private List<string> fuseRights;

        public UserRightsMessageComposer(List<string> list)
        {
            this.fuseRights = list;
        }

        public override void Write()
        {
            _data.Add(fuseRights.Count);

            foreach (var fuse in fuseRights)
            {
                _data.Add(fuse);
            }
        }
    }
}
