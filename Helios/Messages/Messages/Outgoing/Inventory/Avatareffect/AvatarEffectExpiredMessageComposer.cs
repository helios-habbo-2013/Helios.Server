using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class AvatarEffectExpiredMessageComposer : IMessageComposer
    {
        private int effectId;

        public AvatarEffectExpiredMessageComposer(int effectId)
        {
            this.effectId = effectId;
        }

        public override void Write()
        {
            this.AppendInt32(effectId);
        }

        public override int HeaderId => 463;
    }
}