using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class UserInfoMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (!avatar.Authenticated)
                return;

            avatar.Send(new UserInfoComposer(avatar));
        }

        public int HeaderId => 7;
    }
}
