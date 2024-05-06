using Helios.Storage.Models.Room;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class RightsListMessageComposer : IMessageComposer
    {
        private int roomId;
        private List<RoomRightsData> rightsList;

        public RightsListMessageComposer(int roomId, List<RoomRightsData> rightsList)
        {
            this.roomId = roomId;
            this.rightsList = rightsList;
        }

        public override void Write()
        {
            _data.Add(this.roomId);
            _data.Add(rightsList.Count);

            foreach (var right in rightsList)
            {
                _data.Add(right.AvatarId);
                _data.Add(right.AvatarData.Name);
            }
        }
    }
}
