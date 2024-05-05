using Helios.Messages.Outgoing;
using Helios.Storage.Access;
using System;

namespace Helios.Game
{
    public class Subscription : ILoadable
    {
        #region Fields

        private Avatar avatar;

        #endregion

        #region Properties

        public SubscriptionData Data { get; set; }

        #endregion

        #region Constructors

        public Subscription(Avatar avatar)
        {
            this.avatar = avatar;
        }

        public void Load()
        {
            this.Data = SubscriptionDao.GetSubscription(avatar.Details.Id);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Extend club days by months
        /// </summary>
        public void AddMonths(int months)
        {
            DateTime startTime;

            if (avatar.IsSubscribed)
                startTime = Data.ExpireDate;
            else
                startTime = DateTime.Now;

            if (Data == null)
            {
                var nextGiftDate = DateTime.Now;

                switch (ValueManager.Instance.GetString("club.gift.interval.type"))
                {
                    case "MONTH":
                        nextGiftDate = nextGiftDate.AddMonths(ValueManager.Instance.GetInt("club.gift.interval"));
                        break;
                    case "DAY":
                        nextGiftDate = nextGiftDate.AddDays(ValueManager.Instance.GetInt("club.gift.interval"));
                        break;
                }

                Data = new SubscriptionData
                {
                    SubscribedDate = DateTime.Now,
                    ExpireDate = startTime.AddMonths(months),
                    AvatarId = avatar.Details.Id,
                    GiftDate = nextGiftDate
                };

                SubscriptionDao.AddSubscription(Data);
            }
            else
            {
                Data.ExpireDate = startTime.AddMonths(months);
                SubscriptionDao.SaveSubscriptionExpiry(avatar.Details.Id, Data.ExpireDate);
            }

            Update();
        }

        /// <summary>
        /// Send packets to update club
        /// </summary>
        public void Update()
        {
            avatar.Send(new UserRightsMessageComposer(avatar.IsSubscribed ? 2 : 0, 1));
            avatar.Send(new ScrSendUserInfoComposer(avatar.Subscription.Data));
        }

        /// <summary>
        /// Refresh subscription data object
        /// </summary>
        public void Refresh()
        {
            SubscriptionDao.Refresh(Data);
        }

        /// <summary>
        /// Count the membership days when user logs on/off
        /// </summary>
        public void CountMemberDays()
        {
            if (avatar.IsSubscribed)
            {
                Data.SubscriptionAge += (long)DateTime.Now.Subtract(Data.SubscriptionAgeLastUpdated).TotalSeconds;
                Data.SubscriptionAgeLastUpdated = DateTime.Now;

                SubscriptionDao.SaveSubscriptionAge(avatar.Details.Id, Data.SubscriptionAge, Data.SubscriptionAgeLastUpdated);
            }
        }

        #endregion
    }
}
