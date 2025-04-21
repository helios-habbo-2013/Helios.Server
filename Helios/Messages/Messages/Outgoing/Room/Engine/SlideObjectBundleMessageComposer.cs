using Helios.Game;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class SlideObjectBundleMessageComposer : IMessageComposer
    {
        private Item roller;
        private List<RollingData> rollingItems;
        private RollingData rollingEntity;

        public SlideObjectBundleMessageComposer(Item roller, List<RollingData> rollingItems, RollingData rollingEntity)
        {
            this.roller = roller;
            this.rollingItems = rollingItems;
            this.rollingEntity = rollingEntity;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 230;
    }
}