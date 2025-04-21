using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class SetActivatedBadgesMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {

        }

        public int HeaderId => 158;
    }
}