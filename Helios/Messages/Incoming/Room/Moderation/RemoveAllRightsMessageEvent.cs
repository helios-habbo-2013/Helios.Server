using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class RemoveAllRightsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = avatar.RoomUser.Room;

            if (room == null || !room.RightsManager.IsOwner(avatar.Details.Id))
            {
                return;
            }

            using (var context = new StorageContext())
            {
                var rightsList = context.GetRoomRights(room.Data.Id);

                foreach (var toRemove in rightsList)
                {
                    room.RightsManager.RemoveRights(toRemove.AvatarData.Id, false);

                    avatar.Send(new RemoveRightsMessageComposer(room.Data.Id, toRemove.AvatarData.Id));
                }

                context.ClearRoomRights(room.Data.Id);
            }
        }

        public int HeaderId => -1;
    }
}
