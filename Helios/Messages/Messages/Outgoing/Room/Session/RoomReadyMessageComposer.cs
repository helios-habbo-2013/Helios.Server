using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class RoomReadyMessageComposer : IMessageComposer
    {
        private string _model;
        private int _roomId;

        public RoomReadyMessageComposer(string model, int id)
        {
            this._model = model;
            this._roomId = id;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(this._model);
            this.AppendInt32(this._roomId);
        }

        public override int HeaderId => 69;
    }
}