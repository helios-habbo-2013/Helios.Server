using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class AvatarEffectAddedMessageComposer : IMessageComposer
    {
        private Effect effect;

        public AvatarEffectAddedMessageComposer(Effect effect)
        {
            this.effect = effect;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 461;
    }
}