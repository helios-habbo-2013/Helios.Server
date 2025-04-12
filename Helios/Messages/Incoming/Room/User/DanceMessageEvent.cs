using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class DanceMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            if (avatar.RoomUser.IsSitting)
                return;

            int danceId = request.ReadInt();

            avatar.RoomUser.DanceId = danceId;
            avatar.RoomUser.Room.Send(new DanceMessageComposer(avatar.RoomEntity.InstanceId, danceId));
        }

        public int HeaderId => -1;
    }
}