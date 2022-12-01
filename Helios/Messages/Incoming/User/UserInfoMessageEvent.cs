﻿using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class UserInfoMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (!player.Authenticated)
                return;

            player.Send(new UserInfoComposer(player));
            player.Send(new WelcomeUserComposer());
            player.Send(new AllowancesComposer());
        }
    }
}
