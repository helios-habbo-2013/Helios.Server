﻿using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class StopTypingMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            player.RoomUser.TimerManager.ResetSpeechBubbleTimer();
            player.Send(new TypingStatusComposer(player.RoomUser.InstanceId, false));
        }
    }
}