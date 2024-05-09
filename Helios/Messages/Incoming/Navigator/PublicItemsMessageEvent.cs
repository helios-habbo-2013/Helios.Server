using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;

namespace Helios.Messages.Incoming
{
    class PublicItemsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            using (var context = new GameStorageContext())
            {
                avatar.Send(new PublicItemsComposer(context.GetPublicItems()));
            }
        }
    }
}
