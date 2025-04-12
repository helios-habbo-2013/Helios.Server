using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages
{
    class ThrowDiceMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int itemId = request.ReadInt();

            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null)
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null)
                return;

            if (item.Definition.HasBehaviour(ItemBehaviour.REQUIRES_RIGHTS_FOR_INTERACTION))
            {
                if (!room.RightsManager.HasRights(avatar.Details.Id))
                    return;
            }

            item.Interactor.OnInteract(avatar);
        }

        public int HeaderId => -1;
    }
}
