﻿namespace Helios.Messages.Outgoing
{
    class YouAreControllerComposer : IMessageComposer
    {
        private int rightsLevel;

        public YouAreControllerComposer(int rightsLevel)
        {
            this.rightsLevel = rightsLevel;
        }

        public override void Write()
        {
            m_Data.Add(rightsLevel);
        }
    }
}
