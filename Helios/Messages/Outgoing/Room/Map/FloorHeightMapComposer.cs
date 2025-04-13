namespace Helios.Messages.Outgoing
{
    class FloorHeightMapComposer : IMessageComposer
    {
        private string heightmap;

        public FloorHeightMapComposer(string heightmap)
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
