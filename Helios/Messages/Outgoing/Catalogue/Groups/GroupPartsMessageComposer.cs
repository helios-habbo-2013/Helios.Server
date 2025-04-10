using Helios.Game;
using Helios.Storage.Models.Room;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing.Catalogue.Groups
{
    public class GroupPartsMessageComposer : IMessageComposer
    {
        private List<RoomData> roomList;

        public GroupPartsMessageComposer(List<RoomData> roomList)
        {
            this.roomList = roomList;
        }

        public override void Write()
        {
            _data.Add(GroupManager.Cost);
            _data.Add(this.roomList.Count);

            foreach (var room in roomList)
            {
                _data.Add(room.Id);
                _data.Add(room.Name);
                _data.Add(false);
            }

            _data.Add(5);
            _data.Add(10);
            _data.Add(3);
            _data.Add(4);
            _data.Add(25);
            _data.Add(17);
            _data.Add(5);
            _data.Add(25);
            _data.Add(17);
            _data.Add(3);
            _data.Add(29);
            _data.Add(11);
            _data.Add(4);
            _data.Add(0);
            _data.Add(0);
            _data.Add(0);
        }
    }
}
