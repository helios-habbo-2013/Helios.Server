using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetCreditsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Currency.UpdateCredits();
            avatar.Currency.UpdateCurrencies();
        }

        public int HeaderId => 8;
    }
}
