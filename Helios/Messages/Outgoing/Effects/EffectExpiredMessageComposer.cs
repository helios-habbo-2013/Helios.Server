using Helios.Game;
using System;
using System.Collections.Generic;
using System.Text;

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
            m_Data.Add(effectId);
        }
    }
}
