using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class MessengerRequestErrorComposer : IMessageComposer
    {
        private MessengerRequestError messageRequestError;
        public MessengerRequestErrorComposer(MessengerRequestError messageRequestError)
        {
            this.messageRequestError = messageRequestError;
        }

        public override void Write()
        {
            _data.Add((int)messageRequestError); // error code if enum error specified wasn't found by client
            _data.Add((int)messageRequestError);
        }

        public override int HeaderId => 260;
    }
}
