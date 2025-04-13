using Helios.Game;
using Helios.Storage.Models.Room;
using static Helios.Game.FuserightManager;

namespace Helios.Messages.Outgoing
{
    class RoomInfoComposer : IMessageComposer
    {
        public RoomData room;
        private bool isLoading;
        private bool checkEntry;

        public RoomInfoComposer(RoomData roomData)
        {
            this.room = roomData;
            this.isLoading = true;
            this.checkEntry = true;
        }

        public RoomInfoComposer(RoomData roomData, bool isLoading, bool checkEntry)
        {
            this.room = roomData;
            this.isLoading = isLoading;
            this.checkEntry = checkEntry;
        }

        public override void Write()
        {
            _data.Add(isLoading);
            _data.Add(room.Id);
            this.AppendBoolean(this.checkEntry);

            this.AppendStringWithBreak(room.Name);
            this.AppendStringWithBreak(room.OwnerData == null ? string.Empty : room.OwnerData.Name);
            this.AppendInt32((int)room.Status);
            this.AppendInt32(room.UsersNow);
            this.AppendInt32(room.UsersMax);
            this.AppendStringWithBreak(room.Description);
            this.AppendBoolean(true);
            this.AppendBoolean(room.TradeSetting == 1);
            this.AppendInt32(room.Rating);
            this.AppendInt32(room.Category.Id);
            this.AppendStringWithBreak("");
            this.AppendInt32(room.Tags.Count);

            foreach (var tag in room.Tags)
            {
                this.AppendStringWithBreak(tag.Text);
            }

            this.AppendInt32(0);
            this.AppendInt32(0);
            this.AppendInt32(0);

            this.AppendBoolean(false);
        }

        public override int HeaderId => 454;
    }
}
