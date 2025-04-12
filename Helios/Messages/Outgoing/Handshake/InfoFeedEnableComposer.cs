namespace Helios.Messages.Outgoing
{
    public class InfoFeedEnableComposer : IMessageComposer
    {
        public override void Write()
        {
            _data.Add(true);
            _data.Add(false);
        }

        public override int HeaderId => 517;
    }
}
