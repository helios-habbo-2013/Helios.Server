using Helios.Game;
using Helios.Storage.Models.Room;

namespace Helios.Messages.Outgoing
{
    class RoomInfoComposer : IMessageComposer
    {
        public RoomData roomData;
        private bool isLoading;
        private bool checkEntry;

        public RoomInfoComposer(RoomData roomData)
        {
            this.roomData = roomData;
            this.isLoading = true;
            this.checkEntry = true;
        }

        public RoomInfoComposer(RoomData roomData, bool isLoading, bool checkEntry)
        {
            this.roomData = roomData;
            this.isLoading = isLoading;
            this.checkEntry = checkEntry;
        }

        public override void Write()
        {
            _data.Add(isLoading);
            _data.Add(roomData.Id);
            _data.Add(roomData.Name);
            _data.Add(!roomData.IsOwnerHidden); 
            _data.Add(roomData.OwnerId);
            _data.Add(roomData.OwnerData == null ? string.Empty : roomData.OwnerData.Name);
            _data.Add((int)roomData.Status);
            _data.Add(roomData.UsersNow);
            _data.Add(roomData.UsersMax);
            _data.Add(roomData.Description);
            _data.Add(0);
            _data.Add(roomData.TradeSetting); // can category trade?
            _data.Add(roomData.Rating);
            _data.Add(0);
            _data.Add(roomData.Category.Id);
            _data.Add(roomData.GroupData != null ? roomData.GroupData.Id : 0);

            if (roomData.GroupData != null)
            {
                _data.Add(roomData.GroupData.Name);
                _data.Add(roomData.GroupData.Badge);
            }
            else
            {

                _data.Add("");
                _data.Add("");
            }

            _data.Add("");

            _data.Add(roomData.Tags.Count);

            foreach (var tag in roomData.Tags)
            {
                _data.Add(tag.Text);
            }


            _data.Add(0);
            _data.Add(0);
            _data.Add(0);
            _data.Add(true);
            _data.Add(true);
            _data.Add(0);
            _data.Add(0);

            /*
            this._SafeStr_10153 = k.readBoolean(); == CHECK ENTRY
            this._SafeStr_10154 = k.readBoolean();
            this._SafeStr_10155 = k.readBoolean();
            var _local_2:Boolean = k.readBoolean();
            */

            _data.Add(checkEntry);
            _data.Add(false); // ??
            _data.Add(false); // ??
            _data.Add(roomData.IsMuted); // ??

            _data.Add(0); // ??
            _data.Add(0); // ??
            _data.Add(0); // ??
            _data.Add(true); // whether you can mute room
        }

        public int HeaderId => -1;
    }
}
