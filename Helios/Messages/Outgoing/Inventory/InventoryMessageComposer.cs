using System.Collections.Generic;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class InventoryMessageComposer : IMessageComposer
    {
        private string type;
        private List<Item> items;

        public InventoryMessageComposer(string type, List<Item> items1)
        {
            this.type = type;
            this.items = items1;
        }

        public override void Write()
        {
            int i = 0;
            this.AppendInt32(items.Count);

            foreach (var item in items)
            {
                Serialize(this, item, i++);
            }

            this.AppendInt32(items.Count);
        }

        public static void Serialize(IMessageComposer composer, Item item, int stripSlotId = 0)
        {
            composer.AppendInt32(item.Id);
            composer.AppendInt32(0);
            composer.AppendStringWithBreak(item.Definition.Type.ToUpper());
            composer.AppendInt32(item.Id);
            composer.AppendInt32(item.Definition.Data.SpriteId);

            switch (item.Definition.Data.Sprite)
            {
                case "landscape":
                    composer.Data.Add(4);
                    break;
                case "wallpaper":
                    composer.Data.Add(2);
                    break;
                case "floor":
                    composer.Data.Add(3);
                    break;
                case "poster":
                    composer.Data.Add(6);
                    break;
                default:
                    composer.Data.Add(1);
                    break;
            }

            item.Interactor.WriteExtraData(composer, true);

            composer.AppendBoolean(item.Definition.Data.IsRecyclable);
            composer.AppendBoolean(item.Definition.Data.IsTradable);
            composer.AppendBoolean(item.Definition.Data.IsStackable);
            composer.AppendBoolean(item.Definition.Data.IsSellable);
            composer.Data.Add(-1); // embed room id

            if (!item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
            {
                composer.Data.Add("");
                composer.Data.Add(-1); // todo: sprite code for wrapping
            }
        }

        public override int HeaderId => 140;
    }
}