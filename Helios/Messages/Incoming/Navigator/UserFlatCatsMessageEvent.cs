using Helios.Game;
using Helios.Network.Streams;
using Helios.Messages.Outgoing;

namespace Helios.Messages.Incoming
{
    public class UserFlatCatsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new UserFlatCatsComposer(NavigatorManager.Instance.GetCategories(avatar.Details.Rank)));
        }
    }
}
