namespace Helios.Messages.Outgoing
{
    class RoomScoreComposer : IMessageComposer
    {
        private int score;

        public RoomScoreComposer(int score)
        {
            this.score = score;
        }

        public override void Write()
        {
            this.AppendInt32(score);
        }

        public override int HeaderId => 345;
    }
}
