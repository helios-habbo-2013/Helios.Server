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
            m_Data.Add(itemId.ToString());
            m_Data.Add($"{stickieData.Colour} {stickieData.Message}");
        }
    }
}
