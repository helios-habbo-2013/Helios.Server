using System;

namespace Helios.Storage.Database.Data
{
    public class ItemDefinitionData
    {
        public virtual int Id { get; set; }
        public virtual string Sprite { get; set; }
        public virtual string Name { get; set; }
        public virtual int SpriteId { get; set; }
        public virtual int Length { get; set; }
        public virtual int Width { get; set; }
        public virtual double TopHeight { get; set; }
        public virtual int MaxStatus { get; set; }
        public virtual string Behaviour { get; set; }
        public virtual string Interactor { get; set; }
        public virtual bool IsTradable { get; set; }
        public virtual bool IsRecyclable { get; set; }
        public virtual bool IsStackable { get; set; }
        public virtual bool IsSellable { get; set; }
        public virtual string DrinkIds { get; set; }
    }
}
