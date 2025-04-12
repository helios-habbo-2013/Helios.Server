using Helios.Game;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Helios.Storage.Models.Avatar;

namespace Helios.Messages.Outgoing
{
    public class WallItemsComposer : IMessageComposer
    {
        private List<AvatarData> owners;
        private List<Item> wallItems;

        public WallItemsComposer(ConcurrentDictionary<int, Item> items)
        {
            wallItems = items.Where(x => x.Value.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM)).Select(x => x.Value).ToList();
            owners = wallItems.GroupBy(x => x.Data.OwnerId).Select(p => p.First().Data.OwnerData).ToList(); // Create distinct list of room owners
        }

        public override void Write()
        {
            _data.Add(owners.Count);

            foreach (AvatarData avatarData in owners)
            {
                _data.Add(avatarData.Id);
                _data.Add(avatarData.Name);
            }

            _data.Add(wallItems.Count);

            foreach (Item item in wallItems)
            {
                Serialize(this, item);
                _data.Add(item.Data.OwnerId);
            }
        }

        public static void Serialize(IMessageComposer composer, Item item)
        {
            composer.Data.Add(item.Id.ToString());
            composer.Data.Add(item.Definition.Data.SpriteId);
            composer.Data.Add(item.Data.WallPosition);
            composer.Data.Add((string) item.Data.ExtraData);
            composer.Data.Add(-1);
            composer.Data.Add(item.Definition.Data.MaxStatus > 1 ? 1 : 0);
        }

        public int HeaderId => -1;
    }
}
