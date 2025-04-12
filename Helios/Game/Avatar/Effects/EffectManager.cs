using Helios.Messages.Outgoing;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Effect;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Helios.Game
{
    public class EffectManager : ILoadable
    {
        #region Properties

        private Avatar avatar;
        public ConcurrentDictionary<int, Effect> Effects { get; set; }

        #endregion

        #region Constructor

        public EffectManager(Avatar avatar)
        {
            this.avatar = avatar;
        }

        #endregion

        #region Public methods

        public void Load()
        {
            Effects = new ConcurrentDictionary<int, Effect>();

            using (var context = new StorageContext())
            {
                foreach (var effectData in context.GetUserEffects(avatar.EntityData.Id))
                {
                    Effect effect = new Effect(effectData);
                    Effects.TryAdd(effect.Id, effect);
                }
            }

            // avatar.Send(new EffectsMessageComposer(new List<Effect>(Effects.Values)));
        }

        /// <summary>
        /// Add effect to collection
        /// </summary>
        public void AddEffect(EffectData effectData)
        {
            var effect = new Effect(effectData);

            avatar.Send(new EffectAddedMessageComposer(effect));
            Effects.TryAdd(effect.Id, effect);
        }

        #endregion
    }
}
