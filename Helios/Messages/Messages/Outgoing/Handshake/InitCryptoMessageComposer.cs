using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class InitCryptoMessageComposer : IMessageComposer
    {
        private string _secretKey;
        private bool _encryptionEnabled;

        public InitCryptoMessageComposer(string secretKey, bool encryptionEnabled)
        {
            _secretKey = secretKey;
            _encryptionEnabled = encryptionEnabled;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(this._secretKey);
            this.AppendBoolean(this._encryptionEnabled);
        }

        public override int HeaderId => 277;
    }
}