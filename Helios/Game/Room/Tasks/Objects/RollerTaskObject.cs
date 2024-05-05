using Helios.Messages.Outgoing;
using System.Linq;
using System.Threading.Tasks;

namespace Helios.Game
{
    public class RollerTaskObject : ITaskObject
    {
        #region Properties

        public double TaskProcessTime { get { return 2; } }
        //public List<RollingData> RollingItems { get; set; }
        //public IEntity RollingEntity { get; set; }
        private RollerEntry _rollerEntry { get; set; }

        #endregion

        #region Override Properties

        public override bool RequiresTick => true;

        #endregion

        #region Constructor

        public RollerTaskObject(Item item) : base(item) { }

        #endregion

        #region Public methods

        public override void OnTick()
        {
            _rollerEntry = new RollerEntry(this.Item);

            var roller = this.Item;

            if (roller.CurrentTile == null)
            {
                return;
            }

            var rollerTile = roller.CurrentTile;

            // Process items on rollers
            foreach (var item in rollerTile.GetTileItems())
            {
                if (!item.Position.Equals(rollerTile.Position)) // Only roll items placed on the roller
                {
                    continue;
                }

                if (item.Definition.HasBehaviour(ItemBehaviour.ROLLER))
                {
                    continue;
                }

                if (item.IsRolling || _rollerEntry.RollingItems.Any(x => x.Item == item))
                {
                    continue;
                }

                RoomTaskManager.RollerItemTask.TryGetRollingData(item, roller, this.Item.Room, out Position nextPosition);

                if (nextPosition != null)
                {
                    //itemsRolling[item] = new Tuple<Item, Position>(roller, nextPosition);

                    item.RollingData = new RollingData
                    {
                        Item = item,
                        Roller = roller,
                        FromPosition = item.Position.Copy(),
                        NextPosition = nextPosition.Copy()
                    };

                    _rollerEntry.RollingItems.Add(item.RollingData);
                }
            }

            // Process entities on rollers
            var rollerEntities = rollerTile.Entities;

            if (rollerEntities != null && rollerEntities.Count > 0)
            {
                var entity = rollerEntities.Values.FirstOrDefault();

                if (!entity.RoomEntity.IsRolling && _rollerEntry.RollingEntity == null)
                {
                    RoomTaskManager.RollerEntityTask.TryGetRollingData(entity, roller, this.Item.Room, out Position nextPosition);

                    if (nextPosition != null)
                    {
                        entity.RoomEntity.RollingData = new RollingData
                        {
                            Entity = entity,
                            Roller = roller,
                            FromPosition = entity.RoomEntity.Position.Copy(),
                            NextPosition = nextPosition.Copy()
                        };

                        // entitiesRolling[entity] = new Tuple<Item, Position>(roller, nextPosition);
                        _rollerEntry.RollingEntity = entity.RoomEntity.RollingData;
                    }
                }
            }

            // Perform roll animation for entity
            if (_rollerEntry.RollingEntity?.Entity is IEntity rollingEntity)
            {
                RoomTaskManager.RollerEntityTask.DoRoll(rollingEntity, this.Item, this.Item.Room, rollingEntity.RoomEntity.Position, rollingEntity.RoomEntity.RollingData.NextPosition);
            }

            // Perform roll animation for item
            foreach (var kvp in _rollerEntry.RollingItems)
            {
                var rollingItem = kvp.Item;

                if (!rollingItem.IsRollingBlocked)
                {
                    RoomTaskManager.RollerItemTask.DoRoll(rollingItem, this.Item, this.Item.Room, rollingItem.Position, rollingItem.RollingData.NextPosition);
                }

                rollingItem.Save();
            }
        }

        public override void OnTickComplete()
        {
            var rollingItems = _rollerEntry.RollingItems;
            var rollingEntity = _rollerEntry.RollingEntity;

            rollingItems.RemoveAll(item => item.Item.IsRollingBlocked);

            if (rollingItems.Count > 0 || rollingEntity != null)
            {
                this.Item.Room.Send(new SlideObjectBundleComposer(_rollerEntry.Roller, rollingItems, rollingEntity));

                //var itemsRolling = new Dictionary<Item, Tuple<Item, Position>>();
                //var itemsRolling = new List<RollingData>();

                // var entitiesRolling = new Dictionary<IEntity, Tuple<Item, Position>>();

                // Delay after rolling finished
                int delay = (int)(double)(TaskProcessTime * 0.4 * 1000);

                Task.Delay(delay).ContinueWith(t =>
                {
                    foreach (RollingData rollingData in rollingItems)
                    {
                        rollingData.Item.IsRollingBlocked = false;
                        rollingData.Item.RollingData = null;
                    }

                    if (rollingEntity?.Entity is IEntity e)
                    {
                        if (e?.RoomEntity?.RollingData != null)
                        {
                            e.RoomEntity.InteractItem();//getRoomUser().invokeItem(null, true);
                            e.RoomEntity.RollingData = null;
                        }
                    }
                });
            }

            TicksTimer = RoomTaskManager.GetProcessTime(TaskProcessTime);
        }

        #endregion
    }
}