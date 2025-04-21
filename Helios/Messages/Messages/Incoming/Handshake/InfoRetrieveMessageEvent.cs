using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class InfoRetrieveMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (!avatar.Authenticated)
                return;

            avatar.Send(new BadgesComposer());
            avatar.Send(new UserObjectComposer(avatar));
        }

        public int HeaderId => 7;
    }
}