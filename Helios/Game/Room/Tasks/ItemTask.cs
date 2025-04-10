using System;
using Helios.Util.Extensions;
using System.Collections.Concurrent;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Serilog;

namespace Helios.Game
{
    public class ItemTask : RoomTask
    {
        #region Fields

        #endregion

        private readonly ConcurrentQueue<DefaultTaskObject> tickedItems;

        /// <summary>
        /// Set task interval, which is 500ms
        /// </summary>
        public override int Interval => 500;

        /// <summary>
        /// Constructor for the item task
        /// </summary>
        public ItemTask(Room room) : base(room)
        {
            this.tickedItems = new ConcurrentQueue<DefaultTaskObject>(); 
        }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        public override void Run(object sender, ElapsedEventArgs e)
        {
            try
            {
                var queuedItems = new List<DefaultTaskObject>();
                
                // Queue all items that has a task object attached to its interactor
                queuedItems.AddRange([.. Room.ItemManager.Items.Values.Where(x => x.Interactor?.TaskObject != null).Select(x => x.Interactor?.TaskObject)]);

                // Queue all avatars 
                queuedItems.AddRange([.. Room.EntityManager.GetEntities<Avatar>().Where(x => x.RoomEntity?.TaskObject != null).Select(x => x.RoomEntity?.TaskObject)]);

                // Queue all pets (soon)(tm) 
                queuedItems.AddRange([.. Room.EntityManager.GetEntities<Pet>().Where(x => x.RoomEntity?.TaskObject != null).Select(x => x.RoomEntity?.TaskObject)]);

                // Queue all bots (soon)(tm) 
                queuedItems.AddRange([.. Room.EntityManager.GetEntities<Bot>().Where(x => x.RoomEntity?.TaskObject != null).Select(x => x.RoomEntity?.TaskObject)]);

                foreach (var taskObject in queuedItems)
                {
                    if (taskObject.RequiresTick)
                    {
                        if (taskObject.CanTick())
                        {
                            if (!tickedItems.Contains(taskObject))
                                tickedItems.Enqueue(taskObject);
                        }
                    }

                    if (taskObject.EventQueue.Count > 0)
                        taskObject.TryTickState();
                }

                foreach (var taskObject in tickedItems)
                    taskObject.OnTick();

                foreach (var taskObject in tickedItems.Dequeue())
                    taskObject.OnTickComplete();
            }
            catch (Exception ex)
            {
                Log.ForContext<ItemTask>().Error(ex, "Item tick task crashed: ");
            }
        }
    }
}
