using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class AccountPreferencesMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (!avatar.Authenticated)
                return;


        }

        public int HeaderId => 228;
    }
}
