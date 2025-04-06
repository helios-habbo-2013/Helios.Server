using Helios.Storage.Models.Misc;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class NavigatorSettingsComposer : IMessageComposer
    {
        private int homeRoomId;

        public NavigatorSettingsComposer(int homeRoomId)
        {
            this.homeRoomId = homeRoomId;
        }

        public override void Write()
        {
            _data.Add(this.homeRoomId);
        }
    }
}