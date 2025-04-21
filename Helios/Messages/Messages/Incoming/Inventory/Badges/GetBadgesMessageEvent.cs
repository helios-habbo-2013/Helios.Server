using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetBadgesMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {

        }

        public int HeaderId => 157;
    }
}