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
    class ApplyDecorationMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null || !room.IsOwner(avatar.Details.Id))
                return;

            Item item = avatar.Inventory.GetItem(request.ReadInt());

            if (item == null || item.Definition.InteractorType != InteractorType.DECORATION)
                return;

            switch (item.Definition.Data.Sprite)
            {
                case "floor":
                    room.Data.Floor = item.Interactor.GetExtraData().ToString();
                    room.Send(new RoomPropertyComposer("floor", room.Data.Floor));
                    break;
                case "wallpaper":
                    room.Data.Wallpaper = item.Interactor.GetExtraData().ToString();
                    room.Send(new RoomPropertyComposer("wallpaper", room.Data.Wallpaper));
                    break;
                case "landscape":
                    room.Data.Landscape = item.Interactor.GetExtraData().ToString();
                    room.Send(new RoomPropertyComposer("landscape", room.Data.Landscape));
                    break;
            }

            avatar.Inventory.RemoveItem(item);
            avatar.Send(new FurniListRemoveComposer(item.Id));

            RoomDao.SaveRoom(room.Data);
            ItemDao.DeleteItem(item.Data);
        }
    }
}
