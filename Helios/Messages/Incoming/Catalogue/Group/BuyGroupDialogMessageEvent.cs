using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Messages.Outgoing.Catalogue.Groups;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using System.Linq;

namespace Helios.Messages.Incoming.Catalogue
{
    class BuyGroupDialogMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new StorageContext())
            {
                var roomList = context.GetUserRooms(avatar.Details.Id)
                    .Where(x => x.GroupId == null)
                    .ToList();

                avatar.Send(new GroupPartsMessageComposer(roomList));
            }

            avatar.Send(new GroupElementsMessageComposer(
                GroupManager.Instance.BadgeManager.Base,
                GroupManager.Instance.BadgeManager.Symbol,
                GroupManager.Instance.BadgeManager.Colour1,
                GroupManager.Instance.BadgeManager.Colour2,
                GroupManager.Instance.BadgeManager.Colour3
            ));
        }

        public int HeaderId => -1;
    }
}
