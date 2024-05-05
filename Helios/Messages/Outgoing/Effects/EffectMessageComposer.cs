namespace Helios.Messages.Outgoing
{
    class EffectMessageComposer : IMessageComposer
    {
        private int instanceId;
        private int effectId;

        public EffectMessageComposer(int instanceId, int effectId)
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
    }
}
