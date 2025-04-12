namespace Helios.Messages.Outgoing
{
    public class InitCryptoComposer : IMessageComposer
    {
        private string secretKey;

        public InitCryptoComposer(string secretKey)
        {
            this.secretKey = secretKey;
        }

        public override void Write()
        {
            _data.Add(this.secretKey);
            _data.Add(false);
        }

        public override int HeaderId => 277;
    }
}
