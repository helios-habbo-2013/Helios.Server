using System;
using System.Collections.Generic;
using System.Text;
using static Helios.Game.DiceInteractor;

namespace Helios.Game
{
    public class DefaultTaskObject : ITaskObject
    {
        #region Constructor

        public DefaultTaskObject(Item item) : base(item) { }

        #endregion

        #region Public methods
        public override void OnTickComplete() { }

        #endregion
    }
}
