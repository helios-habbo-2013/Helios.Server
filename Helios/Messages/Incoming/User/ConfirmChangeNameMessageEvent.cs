using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class ConfirmChangeNameMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new ChangeNameUpdateComposer());
        }
    }
}
