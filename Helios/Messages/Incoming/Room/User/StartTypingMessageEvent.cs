using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class StartTypingMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            avatar.RoomUser.TimerManager.StartSpeechBubbleTimer();
            avatar.Send(new TypingStatusComposer(avatar.RoomUser.InstanceId, true));
        }

        public int HeaderId => -1;
    }
}
