using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class VersionCheckMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.Authenticated)
            {
                player.Connection.Close();
                return;
            }

            var clientVersion = request.ReadString();

            if (clientVersion == Helios.ClientVersion)
                player.Log.Debug($"Received version: {clientVersion}");
            else
                player.Connection.Close();
        }
    }
}
