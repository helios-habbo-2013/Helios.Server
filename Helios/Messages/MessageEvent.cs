using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages
{
    public interface IMessageEvent
    {
        void Handle(Player player, Request request);
    }
}