using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

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

        public int HeaderId => -1;
    }
}
