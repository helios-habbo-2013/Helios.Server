using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetCatalogPageMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var cataloguePage = CatalogueManager.Instance.GetPage(request.ReadInt(), avatar.Details.Rank, avatar.IsSubscribed);

            if (cataloguePage == null)
                return;

            avatar.Send(new CatalogPageMessageComposer(cataloguePage));

            /*
            CatalogueManager.Instance.TryGetBestDiscount(cataloguePage.Data.Id, out var discount);

            if (discount != null)
                avatar.Send(new CatalogueItemDiscountComposer(discount));
            */
        }

        public int HeaderId => 102;
    }
}