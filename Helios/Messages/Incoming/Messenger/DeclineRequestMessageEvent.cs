using Helios.Game;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    public class DeclineRequestMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new StorageContext())
            {
                bool mode = request.ReadBool();
                int amount = request.ReadInt();

                if (!mode)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        int AvatarId = request.ReadInt();

                        if (!avatar.Messenger.HasRequest(AvatarId))
                            continue;

                        context.DeleteRequests(avatar.Details.Id, AvatarId);
                        avatar.Messenger.RemoveRequest(AvatarId);
                    }
                }
                else
                {
                    avatar.Messenger.Requests.Clear();
                    context.DeleteAllRequests(avatar.Details.Id);
                }
            }
        }

        public int HeaderId => 38;
    }
}
