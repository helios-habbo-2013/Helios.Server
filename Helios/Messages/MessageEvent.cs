using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages
{
    public interface IMessageEvent
    {
        int HeaderId { get; }

        void Handle(Avatar avatar, Request request);
    }
}