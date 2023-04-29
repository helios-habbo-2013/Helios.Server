using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;

namespace Helios.Messages.Incoming
{
    class PublicItemsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            avatar.Send(new PublicItemsComposer(NavigatorDao.GetPublicItems()));
        }
    }
}
