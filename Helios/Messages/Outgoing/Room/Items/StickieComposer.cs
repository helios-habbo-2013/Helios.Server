using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class StickieComposer : IMessageComposer
    {
        private int itemId;
        private StickieExtraData stickieData;

        public StickieComposer(int id, StickieExtraData stickieData)
        {
            this.itemId = id;
            this.stickieData = stickieData;
        }

        public override void Write()
        {
            _data.Add(itemId.ToString());
            _data.Add($"{stickieData.Colour} {stickieData.Message}");
        }
    }
}
