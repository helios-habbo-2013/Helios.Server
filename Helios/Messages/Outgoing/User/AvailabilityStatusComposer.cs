namespace Helios.Messages.Outgoing
{
    class AvailabilityStatusComposer : IMessageComposer
    {
        private bool isOpen;
        private bool isTradingEnded;

        public AvailabilityStatusComposer()
        {
            this.isOpen = true;
            this.isTradingEnded = false;
        }

        public override void Write()
        {
            _data.Add(isOpen);
            _data.Add(isTradingEnded);
        }
    }
}
