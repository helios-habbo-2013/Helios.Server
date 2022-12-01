
using Helios.Storage.Database.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Helios.Storage.Database.Access
{
    public class SubscriptionDao
    {

        /// <summary>
        /// Get subscription by user id
        /// </summary>
        public static SubscriptionData GetSubscription(int userId)
        {
            using (var context = new StorageContext())
            {
                return context.SubscriptionData.SingleOrDefault(x => x.UserId == userId);
            }
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    SubscriptionData subscriptionDataAlias = null;

            //    return session.QueryOver(() => subscriptionDataAlias)
            //        .Where(() => subscriptionDataAlias.UserId == userId)
            //        /*.And(() =>subscriptionDataAlias.ExpireDate > DateTime.Now )*/.SingleOrDefault();
            //}
            return null;

        }

        /// <summary>
        /// Get subscription gifts
        /// </summary>
        public static List<SubscriptionGiftData> GetSubscriptionGifts()
        {
            using (var context = new StorageContext())
            {
                return context.SubscriptionGiftData.ToList();//.SingleOrDefault(x => x.UserId == userId);
            }

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<SubscriptionGiftData>().List() as List<SubscriptionGiftData>;
            //}
            return null;

        }

        /// <summary>
        /// Create subscription by user id
        /// </summary>
        public static void AddSubscription(SubscriptionData subscriptionData)
        {
            using (var context = new StorageContext())
            {
                context.SubscriptionData.Add(subscriptionData);
                context.SaveChanges();
            }
                //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                //{
                //    using (var transaction = session.BeginTransaction())
                //    {
                //        try
                //        {
                //            session.SaveOrUpdate(subscriptionData);
                //            transaction.Commit();
                //        }
                //        catch
                //        {
                //            transaction.Rollback();
                //        }
                //    }
                //}

            }

        /// <summary>
        /// Save subscription by user id
        /// </summary>
        public static void SaveSubscriptionExpiry(int userId, DateTime expiry)
        {
            using (var context = new StorageContext())
            {
                context.SubscriptionData.Attach(new SubscriptionData { UserId = userId, ExpireDate = expiry }).Property(x => x.ExpireDate).IsModified = true;
                context.SaveChanges();
            }

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<SubscriptionData>().Where(x => x.UserId == userId).Update(x => new SubscriptionData { ExpireDate = expiry });
            //}
        }

        /// <summary>
        /// Save club duration by user id
        /// </summary>
        public static void SaveSubscriptionAge(int userId, long clubAge, DateTime clubAgeLastUpdate)
        {
            using (var context = new StorageContext())
            {
                var entity = context.SubscriptionData.Attach(new SubscriptionData { UserId = userId, SubscriptionAge = clubAge, SubscriptionAgeLastUpdated = clubAgeLastUpdate });
                entity.Property(x => x.SubscriptionAge).IsModified = true;
                entity.Property(x => x.SubscriptionAgeLastUpdated).IsModified = true;

                context.SaveChanges();
            }

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<SubscriptionData>().Where(x => x.UserId == userId).Update(x => new SubscriptionData { SubscriptionAge = clubAge, SubscriptionAgeLastUpdated = clubAgeLastUpdate });
            //}
        }

        /// <summary>
        /// Refreshes subscription data
        /// </summary>
        public static void Refresh(SubscriptionData data)
        {
            using (var context = new StorageContext())
            {
                context.Attach(data).Reload();
            }
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Refresh(data);
            //}
        }

        /// <summary>
        /// Save how many gifts a user can redeem
        /// </summary>
        public static void SaveGiftsRedeemable(int userId, int giftsRedeemable)
        {
            using (var context = new StorageContext())
            {
                var entity = context.SubscriptionData.Attach(new SubscriptionData { UserId = userId, GiftsRedeemable = giftsRedeemable });
                entity.Property(x => x.GiftsRedeemable).IsModified = true;
                context.SaveChanges();
            }

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<SubscriptionData>().Where(x => x.UserId == userId).Update(x => new SubscriptionData { GiftsRedeemable = giftsRedeemable });
            //}
        }

    }
}
