using Helios.Game;
using Helios.Storage.Models.Navigator;
using Helios.Storage.Models.Room;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Helios.Messages.Outgoing
{
    public class FlatListComposer : IMessageComposer
    {
        private readonly int signifier;
        private readonly List<Room> roomList;
        private readonly PublicItemData promotion;

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
            messageComposer.AppendInt32(room.Id);
            messageComposer.AppendBoolean(false);
            messageComposer.AppendStringWithBreak(room.Name);
            messageComposer.AppendStringWithBreak(room.OwnerData == null ? string.Empty : room.OwnerData.Name);
            messageComposer.AppendInt32((int)room.Status);
            messageComposer.AppendInt32(room.UsersNow);
            messageComposer.AppendInt32(room.UsersMax);
            messageComposer.AppendStringWithBreak(room.Description);
            messageComposer.AppendBoolean(true);
            messageComposer.AppendBoolean(room.TradeSetting == 1);// room.TradeSetting);
            messageComposer.AppendInt32(room.Rating);
            messageComposer.AppendInt32(room.Category.Id);
            messageComposer.AppendString("");

            /*
            if (room.GroupData != null)
            {
                messageComposer.Data.Add(room.GroupData.Id);
                messageComposer.Data.Add(room.GroupData.Name);
                messageComposer.Data.Add(room.GroupData.Badge);
                messageComposer.Data.Add("");
            }
            else
            {

                messageComposer.Data.Add(0);
                messageComposer.Data.Add("");
                messageComposer.Data.Add("");
                messageComposer.Data.Add("");
            }*/

            messageComposer.AppendInt32(room.Tags.Count);

            foreach (var tag in room.Tags)
            {
                messageComposer.AppendString(tag.Text);
            }

            /*
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            */

            /*
            this._SafeStr_14029 = new _SafeStr_2759(k);
            this._SafeStr_14030 = k.readBoolean();
            this._SafeStr_14031 = k.readBoolean();
            this._SafeStr_14032 = k.readString();
            this._SafeStr_14033 = k.readString();
            this._SafeStr_14034 = k._SafeStr_10829();
            */
        }

        public override int HeaderId => 451;
    }
}
