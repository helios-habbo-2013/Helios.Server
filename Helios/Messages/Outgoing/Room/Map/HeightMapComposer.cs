﻿namespace Helios.Messages.Outgoing
{
    class HeightMapComposer : IMessageComposer
    {
        private string heightmap;

        public HeightMapComposer(string heightmap)
        {
            this.heightmap = heightmap;
        }

        public override void Write()
        {
            m_Data.Add(heightmap);
        }
    }
}
