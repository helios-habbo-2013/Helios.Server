using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class SSOTicketMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Authenticated)
            {
                avatar.Connection.Close();
                return;
            }

            var ssoTicket = request.ReadString();

            if (!avatar.TryLogin(ssoTicket))
                avatar.Connection.Disconnect();
        }
    }
}
