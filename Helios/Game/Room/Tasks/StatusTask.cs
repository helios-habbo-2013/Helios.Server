using System;
using Helios.Messages.Outgoing;
using Helios.Util;
using System.Timers;
using Serilog;

namespace Helios.Game
{
    public class StatusTask : RoomTask
    {

        /// <summary>
        /// Set task interval, which is 1000ms for user maintenance
        /// </summary>
        public override int Interval => 1000;

        /// <summary>
        /// Constructor for the entity task
        /// </summary>
        public StatusTask(Room room) : base(room) { }

        /// <summary>
        /// Run method called every 500ms
        /// </summary>
        /// <param name="state">whatever this means??</param>
        public override void Run(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (IEntity entity in Room.Entities.Values)
                {
                    if (entity.RoomEntity.RoomId != Room.Data.Id)
                        continue;

                    ProcessEntity(entity);
                }
            }
            catch (Exception ex)
            {
                Log.ForContext<StatusTask>().Error(ex, "MaintenanceTask failed");
            }
        }

        /// <summary>
        /// Process user inside room
        /// </summary>
        /// <param name="entity">the entity to process</param>
        private void ProcessEntity(IEntity entity)
        {
            if (entity is Avatar avatar)
            {
                if (avatar.RoomUser.TimerManager.SpeechBubbleDate != -1 && DateUtil.GetUnixTimestamp() > avatar.RoomUser.TimerManager.SpeechBubbleDate)
                {
                    avatar.RoomUser.TimerManager.ResetSpeechBubbleTimer();
                    
                    Room.Send(new UserTypingMessageComposer(avatar.RoomUser.InstanceId, false));
                }
            }
        }
    }
}
