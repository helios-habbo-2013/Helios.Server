using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class OpenCatalogueMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new CataloguePagesComposer(player.Details.Rank, player.IsSubscribed));
        }
    }
}
