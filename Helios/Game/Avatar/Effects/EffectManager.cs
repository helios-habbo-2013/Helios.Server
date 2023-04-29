using Helios.Messages.Outgoing;
using Helios.Storage.Database.Access;
using Helios.Storage.Database.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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

            foreach (var effectData in EffectDao.GetUserEffects(avatar.EntityData.Id))
            {
                Effect effect = new Effect(effectData);
                Effects.TryAdd(effect.Id, effect);
            }

            avatar.Send(new EffectsMessageComposer(new List<Effect>(Effects.Values)));
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
