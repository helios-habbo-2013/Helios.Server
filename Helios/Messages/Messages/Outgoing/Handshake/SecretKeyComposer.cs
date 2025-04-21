using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class SecretKeyComposer : IMessageComposer
    {
        private string _secretKey;

        public SecretKeyComposer(string secretKey)
        {
            this._secretKey = secretKey;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(this._secretKey);
        }

        public override int HeaderId => 1;
    }
}