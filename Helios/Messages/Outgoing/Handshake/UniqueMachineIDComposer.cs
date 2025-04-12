namespace Helios.Messages.Outgoing
{
    public class UniqueMachineIDComposer : IMessageComposer
    {
        private readonly string _uniqueId;

        public UniqueMachineIDComposer(string uniqueId)
        {
            _uniqueId = uniqueId;
        }

        public override void Write()
        {
            AppendStringWithBreak(this._uniqueId);
        }

        public override int HeaderId => 439;
    }
}
