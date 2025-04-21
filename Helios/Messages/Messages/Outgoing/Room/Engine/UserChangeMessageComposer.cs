using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class UserChangeMessageComposer : IMessageComposer
    {
        private int v;
        private string figure;
        private string sex;
        private string motto;
        private int achievementPoints;

        public UserChangeMessageComposer(int v, string figure, string sex, string motto, int achievementPoints)
        {
            this.v = v;
            this.figure = figure;
            this.sex = sex;
            this.motto = motto;
            this.achievementPoints = achievementPoints;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 266;
    }
}