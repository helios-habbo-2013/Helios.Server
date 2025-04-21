using Helios.Game;
using Helios.Storage.Models.Room;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class GuestRoomSearchResultComposer : IMessageComposer
    {
        private readonly int searchType;
        private readonly int signifier;
        private readonly string searchQuery;
        private readonly List<Room> roomList;

        public GuestRoomSearchResultComposer(int searchType, int signifier, string searchQuery, List<Room> roomList)
        {
            this.searchType = searchType;
            this.signifier = signifier;
            this.searchQuery = searchQuery;
            this.roomList = roomList;
        }

        public override void Write()
        {
            this.AppendInt32(this.searchType);
            this.AppendInt32(this.signifier);
            this.AppendStringWithBreak(this.searchQuery);
            this.AppendInt32(roomList.Count);

            foreach (Room room in roomList)
            {
                GuestRoomSearchResultComposer.Compose(this, room.Data);
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
            messageComposer.AppendBoolean(room.TradeSetting == 1);
            messageComposer.AppendInt32(room.Rating);
            messageComposer.AppendInt32(room.Category.Id);
            messageComposer.AppendStringWithBreak("");
            messageComposer.AppendInt32(room.Tags.Count);

            foreach (var tag in room.Tags)
            {
                messageComposer.AppendStringWithBreak(tag.Text);
            }

            messageComposer.AppendInt32(0);
            messageComposer.AppendInt32(0);
            messageComposer.AppendInt32(0);

            messageComposer.AppendBoolean(true);
        }

        public override int HeaderId => 451;
    }
}