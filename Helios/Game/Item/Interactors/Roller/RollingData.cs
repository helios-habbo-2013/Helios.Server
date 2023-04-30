using System;
using System.Collections.Generic;
using System.Text;

namespace Helios.Game
{
    public class QueuedRollerData
    {
        #region Properties

        public Item Roller { get; set; }
        public List<RollingData> RollingItems { get; set; }
        public IEntity RollingEntity { get; set; }

        #endregion

        #region Constructor

        public QueuedRollerData(Item roller)
        {
            Roller = roller;
            RollingItems = new List<RollingData>();
        }

        #endregion
    }

    public class RollingData
    {
        #region Properties

        public Item Roller { get; set; }
        public IEntity Entity { get; set; }
        public Item Item { get; set; }
        public Position FromPosition { get; set; }
        public Position NextPosition { get; set; }
        public double DisplayHeight { get; set; }
        public double HeightUpdate { get; set; }

        #endregion
    }
}
