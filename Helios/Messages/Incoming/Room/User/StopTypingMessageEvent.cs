using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class StopTypingMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            avatar.RoomUser.TimerManager.ResetSpeechBubbleTimer();
            avatar.Send(new TypingStatusComposer(avatar.RoomUser.InstanceId, false));
        }

        public int HeaderId => -1;
    }
}