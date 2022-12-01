using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetFurnitureAliasesMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new FurnitureAliasesComposer());
        }
    }
}
