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
            avatar.Send(new WelcomeUserComposer());
            avatar.Send(new AllowancesComposer());
        }
    }
}
