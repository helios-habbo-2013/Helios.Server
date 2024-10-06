using Helios.Game;
using Helios.Network.Streams;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    class UpdateStickieMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int itemId = request.ReadInt();

            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null) // TODO: Fix for staff
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null) // TODO: Staff check
                return;

            StickieExtraData stickieData = item.Interactor.GetJsonObject<StickieExtraData>();

            string colour = request.ReadString();
            string text = request.ReadString().FilterInput(false);

            if (colour != stickieData.Colour || !stickieData.Message.StartsWith(text))
                if (!room.RightsManager.HasRights(avatar.Details.Id))
                    return; // TODO: Staff check

            item.Interactor.SetExtraData(new StickieExtraData
            {
                Colour = colour,
                Message = text
            });
            item.Update();
            item.Save();
        }
    }
}
