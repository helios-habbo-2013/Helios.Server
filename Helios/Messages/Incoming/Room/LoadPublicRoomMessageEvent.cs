﻿using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class LoadPublicRoomMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            request.ReadBoolean();
            var room = RoomManager.Instance.GetRoom(request.ReadInt());

            if (room == null)
                return;

            if (!room.Data.IsPublicRoom)
                return;

            player.Send(new OpenConnectionComposer(room.Data.Id, room.Data.CategoryId));

            room.EntityManager.EnterRoom(player);
        }
    }
}
