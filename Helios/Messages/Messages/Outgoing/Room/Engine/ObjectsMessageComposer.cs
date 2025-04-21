using Helios.Game;
using Helios.Storage.Models.Avatar;
using Helios.Util.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Outgoing
{
    class ObjectsMessageComposer : IMessageComposer
    {
        private List<AvatarData> _owners;
        private List<Item> _floorItems;

        public ObjectsMessageComposer(ConcurrentDictionary<int, Item> items)
        {
            _floorItems = items.Where(x => !x.Value.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM)).Select(x => x.Value).ToList();
            _owners = _floorItems.GroupBy(x => x.Data.OwnerId).Select(p => p.First().Data.OwnerData).ToList(); // Create distinct list of room owners
        }

        public override void Write()
        {
            _data.Add(_owners.Count);

            foreach (AvatarData avatarData in _owners)
            {
                _data.Add(avatarData.Id);
                _data.Add(avatarData.Name);
            }

            _data.Add(_floorItems.Count);

            foreach (Item item in _floorItems)
            {
                Serialize(this, item);
                _data.Add(item.Data.OwnerId);
            }
        }

        public static void Serialize(IMessageComposer composer, Item item)
        {
            composer.Data.Add(item.Id);
            composer.Data.Add(item.Definition.Data.SpriteId);
            composer.Data.Add(item.Position.X);
            composer.Data.Add(item.Position.Y);
            composer.Data.Add(item.Position.Rotation);
            composer.Data.Add(item.Position.Z.ToClientValue());
            composer.Data.Add(0);
            item.Interactor.WriteExtraData(composer);
            composer.Data.Add(-1);
        }

        public override int HeaderId => 32;
    }
}