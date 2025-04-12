using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Outgoing
{
    public class GetCataloguePageMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var cataloguePage = CatalogueManager.Instance.GetPage(request.ReadInt(), avatar.Details.Rank, avatar.IsSubscribed);

            if (cataloguePage == null)
                return;

            avatar.Send(new CataloguePageComposer(cataloguePage));

            CatalogueManager.Instance.TryGetBestDiscount(cataloguePage.Data.Id, out var discount);

            if (discount != null)
                avatar.Send(new CatalogueItemDiscountComposer(discount));
        }

        public int HeaderId => -1;
    }
}
