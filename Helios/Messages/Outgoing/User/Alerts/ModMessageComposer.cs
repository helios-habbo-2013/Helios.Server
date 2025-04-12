namespace Helios.Messages.Outgoing.User.Alerts
{
    class ModMessageComposer : IMessageComposer
    {
        private readonly string message;
        private readonly string url;

        public ModMessageComposer(string message, string url)
        {
            this.message = message;
            this.url = url;
        }

        public override void Write()
        {
            AppendStringWithBreak(message);
            AppendStringWithBreak(url);
        }

        public override int HeaderId => 161;
    }
}
