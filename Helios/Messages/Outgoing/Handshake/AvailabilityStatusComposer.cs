namespace Helios.Messages.Outgoing
{
    public class AvailabilityStatusComposer : IMessageComposer
    {
        public override void Write()
        {
            _data.Add(true);
            _data.Add(false);
        }

        public override int HeaderId => 190;
    }
}
