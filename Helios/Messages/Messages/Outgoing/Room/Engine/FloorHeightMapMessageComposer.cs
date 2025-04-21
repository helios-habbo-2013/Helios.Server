using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class FloorHeightMapMessageComposer : IMessageComposer
    {
        private string heightmap;

        public FloorHeightMapMessageComposer(string heightmap)
        {
            this.heightmap = heightmap;
        }

        public override void Write()
        {
            _data.Add(heightmap);
        }

        public override int HeaderId => 470;
    }
}