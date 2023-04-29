using System;
using System.Collections.Generic;
using System.Text;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class RemoveItemMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            request.ReadInt();
            int itemId = request.ReadInt();

            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null)
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null && item.Data.OwnerId != avatar.Details.Id && !room.IsOwner(avatar.Details.Id)) // TODO: Staff check
                return;

            room.FurnitureManager.RemoveItem(item, avatar);

            var owner = AvatarManager.Instance.GetAvatarById(item.Data.OwnerId);

            if (owner != null)
            {
                owner.Inventory.AddItem(item);
                owner.Send(new FurniListAddComposer(item));
            }
        }
    }
}
