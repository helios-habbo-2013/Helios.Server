using System.Collections.Generic;

namespace Helios.Game
{
    public class RollerEntry
    {
        private Item roller;
        private List<RollingData> rollingItems;
        private RollingData rollingEntity;

        public RollerEntry(Item roller)
        {
            this.roller = roller;
            this.rollingItems = new List<RollingData>();
            this.rollingEntity = null;
        }

        public Item Roller
        {
            get { return roller; }
        }

        public List<RollingData> RollingItems
        {
            get { return rollingItems; }
        }

        public RollingData RollingEntity
        {
            get { return rollingEntity; }
            set { rollingEntity = value; }
        }
    }

}