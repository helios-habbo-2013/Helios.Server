﻿using Helios.Game;
using Helios.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helios.Messages
{
    class ThrowDiceMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int itemId = request.ReadInt();

            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null)
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null)
                return;

            if (item.Definition.HasBehaviour(ItemBehaviour.REQUIRES_RIGHTS_FOR_INTERACTION))
            {
                if (!room.HasRights(player.Details.Id))
                    return;
            }

            item.Interactor.OnInteract(player);
        }
    }
}
