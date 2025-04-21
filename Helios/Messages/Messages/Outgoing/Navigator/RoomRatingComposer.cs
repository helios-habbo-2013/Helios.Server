using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class RoomRatingComposer : IMessageComposer
    {
        private int score;

        public RoomRatingComposer(int score)
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