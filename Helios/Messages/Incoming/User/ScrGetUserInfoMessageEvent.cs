using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class ScrGetUserInfoMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new ScrSendUserInfoComposer(player.Subscription.Data));
        }
    }
}
