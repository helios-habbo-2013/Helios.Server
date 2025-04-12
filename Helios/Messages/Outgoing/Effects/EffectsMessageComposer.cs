using Helios.Game;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class EffectsMessageComposer : IMessageComposer
    {
        private List<Effect> effects;

        public EffectsMessageComposer(List<Effect> effects)
        {
            this.effects = effects;
        }

        public override void Write()
        {
            _data.Add(effects.Count);
        
            foreach (var effect in effects)
            {
                Compose(effect, this);
            }
        }

        internal static void Compose(Effect effect, IMessageComposer composer)
        {
            composer.Data.Add(effect.Id);
            composer.Data.Add(effect.IsCostume ? 1 : 0);
            composer.Data.Add(effect.Duration);
            composer.Data.Add(effect.Data.IsActivated ? effect.Data.Quantity - 1 : effect.Data.Quantity);
            composer.Data.Add(effect.Data.IsActivated ? effect.TimeLeft : -1);
        }

        public override int HeaderId => 460;
    }
}
