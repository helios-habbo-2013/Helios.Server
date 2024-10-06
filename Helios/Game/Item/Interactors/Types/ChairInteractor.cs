using Helios.Util.Extensions;

namespace Helios.Game
{
    public class ChairInteractor : Interactor
    {
        #region Overridden Properties


        #endregion

        public ChairInteractor(Item item) : base(item) { }

        public override void OnStop(IEntity entity)
        {
            entity.RoomEntity.Position.Rotation = Item.Position.Rotation;
            entity.RoomEntity.AddStatus("sit", Item.Definition.Data.TopHeight.ToClientValue());
            entity.RoomEntity.NeedsUpdate = true;
        }
    }
}
