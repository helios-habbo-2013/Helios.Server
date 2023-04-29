using System;
using System.Collections.Generic;
using System.Text;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class PlaceStickieMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int itemId = request.ReadInt();

            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null || !room.HasRights(avatar.Details.Id))
                return;

            Item item = avatar.Inventory.GetItem(itemId);

            if (item == null)
                return;

            string wallPosition = request.ReadString();
            room.FurnitureManager.AddItem(item, wallPosition: wallPosition);

            avatar.Inventory.RemoveItem(item);
            avatar.Send(new FurniListRemoveComposer(item.Id));
        }
    }
}
