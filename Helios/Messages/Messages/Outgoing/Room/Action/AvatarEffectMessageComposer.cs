using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class AvatarEffectMessageComposer : IMessageComposer
    {
        private int instanceId;
        private int effectId;

        public AvatarEffectMessageComposer(int instanceId, int effectId)
        {
            this.instanceId = instanceId;
            this.effectId = effectId;
        }

        public override void Write()
        {
            _data.Add(instanceId);
            _data.Add(effectId);
            _data.Add(0);
        }

        public override int HeaderId => 485;
    }
}