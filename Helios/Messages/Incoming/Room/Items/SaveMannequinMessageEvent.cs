using Helios.Game;
using Helios.Network.Streams;
using Helios.Util.Extensions;

namespace Helios.Messages.Outgoing
{
    class SaveMannequinMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;
            Item item = room.ItemManager.GetItem(request.ReadInt());

            if (item == null || item.Definition.InteractorType != InteractorType.MANNEQUIN)
                return;

            if (string.IsNullOrEmpty(avatar.Details.Figure))
                return;

            if (!room.RightsManager.IsOwner(avatar.Details.Id))
                return;

            string mannequinName = request.ReadString().FilterInput(true);

            if (mannequinName.Length > 30)
                mannequinName = mannequinName.Substring(0, 30);

            item.Interactor.SetJsonObject(new MannequinExtraData()
            {
                Figure = avatar.Details.Figure,
                Gender = avatar.Details.Sex,
                OutfitName = mannequinName
            });

            item.Update();
            item.Save();
        }
    }
}
