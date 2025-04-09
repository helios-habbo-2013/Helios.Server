using Helios.Messages.Outgoing;
using Helios.Storage.Access;
using Helios.Util;
using Helios.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Helios.Game
{
    public class AvatarTaskObject : ITaskObject
    {
        private class AvatarAttribute
        {
            public const string TYPING_STATUS = "TYPING_STATUS";
            public const string EFFECT_EXPIRY = "EFFECT_EXPIRY";
        }

        #region Fields

  
        #endregion

        #region Constructor

        public AvatarTaskObject(IEntity entity) : base(entity) { }

        #endregion

        #region Override Properties

        public override bool RequiresTick => true;

        #endregion

        #region Public methods

        public override void OnTickComplete()
        {
            if (!EventQueue.ContainsKey(AvatarAttribute.TYPING_STATUS))
                QueueEvent(AvatarAttribute.TYPING_STATUS, 1.0, ProcessTypingStatus, null);

            if (!EventQueue.ContainsKey(AvatarAttribute.EFFECT_EXPIRY))
                QueueEvent(AvatarAttribute.EFFECT_EXPIRY, 1.0, ProcessEffectExpiry, null);

            TicksTimer = RoomTaskManager.GetProcessTime(0.5);
        }

        /// <summary>
        /// Process typing status stopped after x seconds
        /// </summary>
        public void ProcessTypingStatus(QueuedEvent queuedEvent)
        {
            if (Entity is Avatar avatar)
            {
                if (avatar.RoomUser.TimerManager.SpeechBubbleDate != -1 && DateUtil.GetUnixTimestamp() > avatar.RoomUser.TimerManager.SpeechBubbleDate)
                {
                    avatar.RoomUser.TimerManager.ResetSpeechBubbleTimer();
                    avatar.RoomUser.Room.Send(new TypingStatusComposer(avatar.RoomUser.InstanceId, false));
                }
            }
        }


        /// <summary>
        /// Process effect expiry
        /// </summary>
        public void ProcessEffectExpiry(QueuedEvent queuedEvent)
        {
            if (Entity is Avatar avatar)
            {
                foreach (var effect in avatar.EffectManager.Effects.Where(x => x.Value.Data.IsActivated && x.Value.Data.ExpiresAt != null && DateTime.Now > x.Value.Data.ExpiresAt).ToList())
                {
                    if (effect.Value.Data.Quantity > 0)
                        effect.Value.Data.Quantity--;

                    effect.Value.Data.ExpiresAt = null;

                    using (var context = new GameStorageContext())
                    {
                        if (effect.Value.Data.Quantity == 0)
                        {
                            avatar.EffectManager.Effects.Remove(effect.Value.Id);
                            context.DeleteEffect(effect.Value.Data);
                        }
                        else
                        {
                            context.UpdateEffect(effect.Value.Data);
                        }
                    }

                    avatar.Send(new EffectExpiredMessageComposer(effect.Value.Id));

                    if (avatar.RoomEntity.EffectId == effect.Value.Id)
                        avatar.RoomEntity.UseEffect(0);
                }
            }
        }

        #endregion
    }
}
