using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GenerateSecretKeyMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Authenticated)
            {
                avatar.Connection.Close();
                return;
            }

            avatar.Send(new SecretKeyComposer("12844543231839046982589043811871065476910599239608903221675649771360705087933"));
        }

        public int HeaderId => 2002;
    }
}
