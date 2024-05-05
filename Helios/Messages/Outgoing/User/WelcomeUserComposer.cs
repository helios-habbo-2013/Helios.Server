namespace Helios.Messages.Outgoing
{
    public class WelcomeUserComposer : IMessageComposer
    {
        public override void Write()
        {
            _data.Add(0);
        }
    }
}
