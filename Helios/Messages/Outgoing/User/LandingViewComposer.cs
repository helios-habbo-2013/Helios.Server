namespace Helios.Messages.Outgoing
{
    public class LandingViewComposer : IMessageComposer
    {
        private string promotion;
        private string catalogueTeaser;
        public LandingViewComposer(string promotion, string catalogueTeaser)
        {
            this.promotion = promotion;
            this.catalogueTeaser = catalogueTeaser;
        }

        public override void Write()
        {
            _data.Add(this.promotion);
            _data.Add(this.catalogueTeaser);
        }
    }
}
