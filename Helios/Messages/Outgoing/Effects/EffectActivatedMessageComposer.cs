using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class EffectActivatedMessageComposer : IMessageComposer
    {
        private Effect effect;

        public EffectActivatedMessageComposer(Effect effect)
        {
            this.effect = effect;
        }

        public override void Write()
        {
            _data.Add(effect.Id);
            _data.Add(effect.Duration);
        }
    }
}
