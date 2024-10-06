using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class ApplyDecorationMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null || !room.RightsManager.IsOwner(avatar.Details.Id))
                return;

            Item item = avatar.Inventory.GetItem(request.ReadInt());

            if (item == null || item.Definition.InteractorType != InteractorType.DECORATION)
                return;

            switch (item.Definition.Data.Sprite)
            {
                case "floor":
                    room.Data.Floor = item.Data.ExtraData.ToString();
                    room.Send(new RoomPropertyComposer("floor", room.Data.Floor));
                    break;
                case "wallpaper":
                    room.Data.Wallpaper = item.Data.ExtraData.ToString();
                    room.Send(new RoomPropertyComposer("wallpaper", room.Data.Wallpaper));
                    break;
                case "landscape":
                    room.Data.Landscape = item.Data.ExtraData.ToString();
                    room.Send(new RoomPropertyComposer("landscape", room.Data.Landscape));
                    break;
            }

            avatar.Inventory.RemoveItem(item);

            using (var context = new GameStorageContext())
            {
                context.SaveRoom(room.Data);
                context.DeleteItem(item.Data);
            }
        }
    }
}
