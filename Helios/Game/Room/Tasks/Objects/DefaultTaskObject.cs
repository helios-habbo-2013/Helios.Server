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
