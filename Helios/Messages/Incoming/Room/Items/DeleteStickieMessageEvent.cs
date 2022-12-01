using System;
using System.Collections.Generic;
using System.Text;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    class DeleteStickieMessageEvent : IMessageEvent
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

            if (item == null && item.Data.OwnerId != player.Details.Id && !room.IsOwner(player.Details.Id)) // TODO: Staff check
                return;

            room.FurnitureManager.RemoveItem(item, player);

            ItemDao.DeleteItem(item.Data);
        }
    }
}
