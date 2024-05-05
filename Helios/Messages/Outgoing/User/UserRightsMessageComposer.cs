namespace Helios.Messages.Outgoing
{
    class UserRightsMessageComposer : IMessageComposer
    {
        private int clubLevel;
        private int rank;

        public UserRightsMessageComposer(int clubLevel, int rank)
        {
            this.clubLevel = clubLevel;
            this.rank = rank;
        }

        public override void Write()
        {
            _data.Add(clubLevel);
            _data.Add(rank);
        }
    }
}
