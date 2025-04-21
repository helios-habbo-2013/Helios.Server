using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetUserFlatCatsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new UserFlatCatsComposer(NavigatorManager.Instance.GetCategories(avatar.Details.Rank)));
        }

        public int HeaderId => 151;
    }
}