using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class EffectAddedMessageComposer : IMessageComposer
    {
        private Effect effect;

        public EffectAddedMessageComposer(Effect effect)
        {
            this.effect = effect;
        }

        public override void Write()
        {
            EffectsMessageComposer.Compose(effect, this);
        }
    }
}
