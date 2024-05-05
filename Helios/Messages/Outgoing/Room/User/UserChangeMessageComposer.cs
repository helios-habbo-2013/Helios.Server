namespace Helios.Messages.Outgoing
{
    public class UserChangeMessageComposer : IMessageComposer
    {
        private int virtualId;
        private string figure;
        private string sex;
        private string motto;
        private int achievementPoints;

        public UserChangeMessageComposer(int v, string figure, string sex, string motto, int achievementPoints)
        {
            this.virtualId = v;
            this.figure = figure;
            this.sex = sex;
            this.motto = motto;
            this.achievementPoints = achievementPoints;
        }

        public override void Write()
        {
            _data.Add(virtualId);
            _data.Add(figure);
            _data.Add(sex);
            _data.Add(motto);
            _data.Add(achievementPoints);
        }
    }
}
