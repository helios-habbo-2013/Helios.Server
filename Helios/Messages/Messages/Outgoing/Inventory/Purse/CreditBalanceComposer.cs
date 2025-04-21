using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class CreditBalanceComposer : IMessageComposer
    {
        private int _credits;

        public CreditBalanceComposer(int credits)
        {
            this._credits = credits;
        }

        public override void Write()
        {
            this.AppendString($"{_credits}.0");
        }

        public override int HeaderId => 6;
    }
}