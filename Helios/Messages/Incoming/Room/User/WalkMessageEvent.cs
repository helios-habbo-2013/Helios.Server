using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class WalkMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (!avatar.RoomUser.WalkingAllowed)
                return;

            int x = request.ReadInt();
            int y = request.ReadInt();

            avatar.RoomUser.Move(x, y);
        }

        public int HeaderId => -1;
    }
}
