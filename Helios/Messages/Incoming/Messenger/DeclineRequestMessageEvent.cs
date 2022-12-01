using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class DeclineRequestMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            bool mode = request.ReadBoolean();
            int amount = request.ReadInt();

            if (!mode)
            {
                for (int i = 0; i < amount; i++)
                {
                    int userId = request.ReadInt();

                    if (!player.Messenger.HasRequest(userId))
                        continue;

                    MessengerDao.DeleteRequests(player.Details.Id, userId);
                    player.Messenger.RemoveRequest(userId);
                }
            } 
            else
            {
                player.Messenger.Requests.Clear();
                MessengerDao.DeleteAllRequests(player.Details.Id);
            }
        }
    }
}
