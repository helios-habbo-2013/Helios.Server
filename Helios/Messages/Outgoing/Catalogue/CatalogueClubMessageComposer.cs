using Helios.Storage.Models.Catalogue;
using System;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class CatalogueClubMessageComposer : IMessageComposer
    {
        private List<CatalogueSubscriptionData> subscriptions;

        public CatalogueClubMessageComposer(List<CatalogueSubscriptionData> subscriptionData)
        {
            this.subscriptions = subscriptionData;
        }

        public override void Write()
        {
            _data.Add(subscriptions.Count);

            /* SENT 2677 / [0][0][0]�
             * [10]u
             * [0][0][0][3]
             * 
             * [0][0][8]F[0][9]DEAL_HC_1[0][0][0]d[0][0][0][0][2][0][0][0][1][0][0][0][1][0][0][0][0][0][0][0][0][0][0][7]�[0][0][0][3][0][0][0][0][0][8]G[0][9]DEAL_HC_3[0][0][1]�[0][0][0][0][2][0][0][0][6][0][0][0][6][0][0][0][0][0][0][0][0][0][0][7]�[0][0][0][3][0][0][0][0][0][8]H[0][9]DEAL_HC_2[0][0][0]�[0][0][0][0][2][0][0][0][3][0][0][0][3][0][0][0][0][0][0][0][0][0][0][7]�[0][0][0][3][0][0][0][0][0][0][1]*/

            foreach (var subscription in subscriptions)
            {
                _data.Add(subscription.Id);
                _data.Add("DEAL_HC_" + subscription.Months);
                _data.Add(subscription.PriceCoins);
                _data.Add(subscription.PriceSeasonal);
                _data.Add((int)subscription.SeasonalType);
                _data.Add(true); // public function get vip():Boolean
                _data.Add(subscription.Months);

                _data.Add(0); // ??
                _data.Add(0); // ??

                var futureDate = DateTime.Now.AddMonths(subscription.Months);

                _data.Add(futureDate.Year);
                _data.Add(futureDate.Month);
                _data.Add(futureDate.Day);
            }

            _data.Add(1);
        }

        public int HeaderId => -1;
    }
}
