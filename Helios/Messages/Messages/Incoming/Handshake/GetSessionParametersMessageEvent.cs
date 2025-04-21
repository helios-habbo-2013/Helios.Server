using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetSessionParametersMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Authenticated)
            {
                avatar.Connection.Close();
                return;
            }

            avatar.Send(new SessionParamsMessageComposer(avatar));
        }

        public int HeaderId => 1817;
    }
}