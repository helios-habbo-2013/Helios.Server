using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    public class DeclineRequestMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            bool mode = request.ReadBoolean();
            int amount = request.ReadInt();

            if (!mode)
            {
                for (int i = 0; i < amount; i++)
                {
                    int AvatarId = request.ReadInt();

                    if (!avatar.Messenger.HasRequest(AvatarId))
                        continue;

                    MessengerDao.DeleteRequests(avatar.Details.Id, AvatarId);
                    avatar.Messenger.RemoveRequest(AvatarId);
                }
            } 
            else
            {
                avatar.Messenger.Requests.Clear();
                MessengerDao.DeleteAllRequests(avatar.Details.Id);
            }
        }
    }
}
