namespace Helios.Messages.Outgoing
{
    class PublicRoomCastsComposer : IMessageComposer
    {
        private int roomId;
        private string ccts;

        public PublicRoomCastsComposer(int roomId, string ccts)
        {
            this.roomId = roomId;
            this.ccts = ccts;
        }

        public override void Write()
        {
            _data.Add(roomId);
            _data.Add(ccts);
            _data.Add(roomId);
        }
    }
}
