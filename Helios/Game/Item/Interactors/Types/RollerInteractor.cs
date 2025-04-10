namespace Helios.Game
{
    public class RollerInteractor : Interactor
    {
        #region Fields

        public RollerTaskObject taskObject;

        #endregion

        #region Overridden Properties

        public override DefaultTaskObject TaskObject => taskObject;

        #endregion

        #region Constructor

        public RollerInteractor(Item item) : base(item)
        {
            this.taskObject = new RollerTaskObject(item);
        }

        #endregion
    }
}
