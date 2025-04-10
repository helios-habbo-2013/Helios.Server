using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Avatar;
using Helios.Util.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Incoming
{
    class SearchMessengerMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new StorageContext())
            {
                List<AvatarData> resultSet = context.SearchMessenger(request.ReadString().FilterInput(), avatar.Details.Id);

                var friends = resultSet.Where(data => avatar.Messenger.HasFriend(data.Id))
                    .Select(data => new MessengerUser(data)).ToList();

                var users = resultSet.Where(data => !avatar.Messenger.HasFriend(data.Id))
                    .Select(data => new MessengerUser(data)).ToList();

                avatar.Send(new SearchMessengerComposer(friends, users));
            }
        }
    }
}
