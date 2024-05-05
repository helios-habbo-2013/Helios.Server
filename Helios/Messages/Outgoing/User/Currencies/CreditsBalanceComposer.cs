namespace Helios.Messages.Outgoing
{
    public class CreditsBalanceComposer : IMessageComposer
    {
        private int credits;

        public CreditsBalanceComposer(int credits)
        {
            this.credits = credits;
        }

        public override void Write()
        {
            _data.Add($"{credits}.0");
        }
    }
}
