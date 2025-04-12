using System.Collections.Generic;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class PromotableRoomsMessageComposer : IMessageComposer
    {
        private List<Room> roomList;

        public PromotableRoomsMessageComposer(List<Room> roomList)
        {
            this.roomList = roomList;
        }

        public override void Write()
        {
            Data.Add(true);
            Data.Add(roomList.Count);

            foreach (Room room in roomList)
            {
                Data.Add(room.Data.Id);
                Data.Add(room.Data.Name);
                Data.Add(false);
            }
        }

        public int HeaderId => -1;
    }
}
