namespace Helios.Messages.Outgoing
{
    class EffectExpiredMessageComposer : IMessageComposer
    {
        private int effectId;

        public EffectExpiredMessageComposer(int effectId)
        {
            this.effectId = effectId;
        }

        public override void Write()
        {
            _data.Add(effectId);
        }
    }
}
