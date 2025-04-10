using Helios.Game;
using Helios.Network.Streams;
using Serilog;

namespace Helios.Messages.Incoming
{
    class VersionCheckMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Authenticated)
            {
                avatar.Connection.Close();
                return;
            }

            var clientVersion = request.ReadString();

            if (clientVersion == Helios.ClientVersion)
                Log.ForContext<VersionCheckMessageEvent>().Debug($"Received version: {clientVersion}");
            else
                avatar.Connection.Close();
        }
    }
}
