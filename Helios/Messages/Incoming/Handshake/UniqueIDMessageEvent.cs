using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class UniqueIDMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Authenticated)
            {
                avatar.Connection.Close();
                return;
            }

            avatar.Send(new UniqueMachineIDComposer("foobar"));
            avatar.Send(new SessionParametersComposer(avatar));
        }

        public int HeaderId => 813;
    }
}
