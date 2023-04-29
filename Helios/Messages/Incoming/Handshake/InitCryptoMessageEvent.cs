using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class InitCryptoMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Authenticated)
            {
                avatar.Connection.Close();
                return;
            }

            avatar.Send(new InitCryptoComposer("1e9d1203d2203d3dd9ddcb192ccf0a01"));
        }
    }
}
