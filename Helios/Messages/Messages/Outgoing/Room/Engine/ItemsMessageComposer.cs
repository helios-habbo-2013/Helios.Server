using Helios.Game;
using Helios.Storage.Models.Avatar;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Outgoing
{
    class ItemsMessageComposer : IMessageComposer
    {
        private List<AvatarData> _owners;
        private List<Item> _wallItems;

        public ItemsMessageComposer(ConcurrentDictionary<int, Item> items)
        {
            _wallItems = items.Where(x => x.Value.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM)).Select(x => x.Value).ToList();
            _owners = _wallItems.GroupBy(x => x.Data.OwnerId).Select(p => p.First().Data.OwnerData).ToList(); // Create distinct list of room owners
        }

        public override void Write()
        {
            _data.Add(_owners.Count);

            foreach (AvatarData avatarData in _owners)
            {
                _data.Add(avatarData.Id);
                _data.Add(avatarData.Name);
            }

            _data.Add(_wallItems.Count);

            foreach (Item item in _wallItems)
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
            composer.Data.Add((string)item.Data.ExtraData);
        }

        public override int HeaderId => 45;
    }
}