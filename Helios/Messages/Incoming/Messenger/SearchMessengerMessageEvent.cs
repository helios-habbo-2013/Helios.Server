using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;
using Helios.Storage.Database.Data;
using Helios.Util.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Incoming
{
    class SearchMessengerMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            List<PlayerData> resultSet = MessengerDao.SearchMessenger(request.ReadString().FilterInput(), player.Details.Id);

            var friends = resultSet.Where(data => player.Messenger.HasFriend(data.Id))
                .Select(data => new MessengerUser(data)).ToList();

            var users = resultSet.Where(data => !player.Messenger.HasFriend(data.Id))
                .Select(data => new MessengerUser(data)).ToList();

            player.Send(new SearchMessengerComposer(friends, users));
        }
    }
}
