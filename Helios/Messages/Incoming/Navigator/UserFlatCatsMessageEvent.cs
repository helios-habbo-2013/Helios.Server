using Helios.Game;
using Helios.Network.Streams;
using Helios.Messages.Outgoing;

namespace Helios.Messages.Incoming
{
    public class UserFlatCatsMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new UserFlatCatsComposer(NavigatorManager.Instance.GetCategories(player.Details.Rank)));
        }
    }
}
