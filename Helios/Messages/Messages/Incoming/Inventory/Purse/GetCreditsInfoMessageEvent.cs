using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetCreditsInfoMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Currency.UpdateCredits();
            avatar.Currency.UpdateCurrencies();
        }

        public int HeaderId => 8;
    }
}