using Helios.Game;
using Helios.Messages.Incoming;
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

            var secretKey = "occ08hj7td8xltcvmr1f1oxmkdbqr1i1";
            var encryptionEnabled = true;

            avatar.Send(new InitCryptoMessageComposer(secretKey, encryptionEnabled));
        }

        public int HeaderId => 206;
    }
}