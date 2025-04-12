using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetGroupBadgesMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new RoomGroupBadgeComposer());
        }

        public int HeaderId => 230;
    }
}
