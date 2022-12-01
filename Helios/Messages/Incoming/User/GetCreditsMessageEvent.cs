using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetCreditsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Currency.UpdateCredits();
            player.Currency.UpdateCurrencies();
        }
    }
}
