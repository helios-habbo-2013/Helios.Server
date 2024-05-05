namespace Helios.Game
{
    public interface IRollerTask<T>
    {
        #region Methods

        void TryGetRollingData(T rollingType, Item roller, Room room, out Position nextPosition);
        void DoRoll(T rollingType, Item roller, Room room, Position fromPosition, Position nextPosition);

        #endregion
    }
}
