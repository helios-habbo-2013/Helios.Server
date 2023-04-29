using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class ScrGetUserInfoMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new ScrSendUserInfoComposer(avatar.Subscription.Data));
        }
    }
}
