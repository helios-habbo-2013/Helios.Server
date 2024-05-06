using Helios.Game;
using Helios.Storage.Models.Navigator;
using Helios.Storage.Models.Room;
using System;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    public class FlatListComposer : IMessageComposer
    {
        private int signifier;
        private List<Room> roomList;
        private PublicItemData promotion;

        public FlatListComposer(int signifier, List<Room> roomList, PublicItemData promotion)
        {
            this.signifier = signifier;
            this.roomList = roomList;
            this.promotion = promotion;
        }

        public override void Write()
        {
            _data.Add(0);
            _data.Add(Convert.ToString(this.signifier));
            _data.Add(roomList.Count);

            foreach (Room room in roomList)
            {
                FlatListComposer.Compose(this, room.Data);
            }

            //m_Data.Add(false);
            _data.Add(promotion != null);

            if (promotion != null)
            {
                PublicItemsComposer.Compose(this, promotion);
            }
        }

        public static void Compose(IMessageComposer messageComposer, RoomData room)
        {
            messageComposer.Data.Add(room.Id);
            messageComposer.Data.Add(room.Name);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(room.OwnerId);
            messageComposer.Data.Add(room.OwnerData == null ? string.Empty : room.OwnerData.Name);
            messageComposer.Data.Add((int)room.Status);
            messageComposer.Data.Add(room.UsersNow);
            messageComposer.Data.Add(room.UsersMax);
            messageComposer.Data.Add(room.Description);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(room.TradeSetting);// room.TradeSetting);
            messageComposer.Data.Add(room.Rating);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(room.Category.Id);
            messageComposer.Data.Add(room.GroupData != null ? room.GroupData.Id : 0);

            if (room.GroupData != null)
            {
                messageComposer.Data.Add(room.GroupData.Name);
                messageComposer.Data.Add(room.GroupData.Badge);
            }
            else
            {

                messageComposer.Data.Add("");
                messageComposer.Data.Add("");
            }

            messageComposer.Data.Add("");
            messageComposer.Data.Add(room.Tags.Count);

            foreach (var tag in room.Tags)
            {
                messageComposer.Data.Add(tag.Text);
            }

            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);

            /*
            this._SafeStr_14029 = new _SafeStr_2759(k);
            this._SafeStr_14030 = k.readBoolean();
            this._SafeStr_14031 = k.readBoolean();
            this._SafeStr_14032 = k.readString();
            this._SafeStr_14033 = k.readString();
            this._SafeStr_14034 = k._SafeStr_10829();
            */
        }
    }
}
