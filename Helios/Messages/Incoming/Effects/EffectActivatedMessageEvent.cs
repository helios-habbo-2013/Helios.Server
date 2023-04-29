using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helios.Messages.Incoming
{
    class EffectActivatedMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int effectId = request.ReadInt();

            if (!avatar.EffectManager.Effects.ContainsKey(effectId))
                return;

            var effect = avatar.EffectManager.Effects[effectId];

            if (!effect.TryActivate())
                return;

            avatar.Send(new EffectActivatedMessageComposer(effect));
        }
    }
}
