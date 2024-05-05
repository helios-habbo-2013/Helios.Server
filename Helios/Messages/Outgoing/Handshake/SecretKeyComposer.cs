namespace Helios.Messages.Outgoing
{
    public class SecretKeyComposer : IMessageComposer
    {
        private string publicKey;

        public SecretKeyComposer(string publicKey)
        {
            this.publicKey = publicKey;
        }

        public override void Write()
        {
            _data.Add(this.publicKey);
        }
    }
}
