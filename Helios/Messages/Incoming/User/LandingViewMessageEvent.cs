using Helios.Game;
using Helios.Network.Streams;
using Helios.Messages.Outgoing;

namespace Helios.Messages.Incoming
{
    class LandingViewMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            string first = request.ReadString();

            if (string.IsNullOrEmpty(first))
            {
                avatar.Send(new LandingViewComposer("", ""));
                return;
            }

            string value = first.Split(',')[1];

            //avatar.Connection.Send(new LandingViewComposer(value, value.Split(';')[0]));
            //avatar.Connection.Send(new LandingViewComposer("2012-11-09 19:00,hstarsa;2012-11-30 12:00,", "hstarsa"));
        }
    }
}
