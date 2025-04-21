using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class HeightMapMessageComposer : IMessageComposer
    {
        private string heightmap;

        public HeightMapMessageComposer(string heightmap)
        {
            this.heightmap = heightmap;
        }

        public override void Write()
        {
            _data.Add(heightmap);
        }

        public override int HeaderId => 31;
    }
}