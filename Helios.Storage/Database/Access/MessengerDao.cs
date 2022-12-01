using Helios.Storage.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Database.Access
{
    public class MessengerDao
    {
        /// <summary>
        /// Search messenger for names starting with the query
        /// </summary>
        /// <returns></returns>
        public static List<PlayerData> SearchMessenger(string query, int ignoreUserId, int searchResultLimit = 30)
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                PlayerData playerDataAlias = null;

                return session.QueryOver<PlayerData>(() => playerDataAlias)
                    //.Where(Restrictions.On<PlayerData>(x => x.Name).IsInsensitiveLike(query, MatchMode.Start))
                    .WhereRestrictionOn(() => playerDataAlias.Name).IsLike(query, MatchMode.Start)
                    .And(() => playerDataAlias.Id != ignoreUserId)
                    .Take(searchResultLimit)
                    .List() as List<PlayerData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.PlayerData.Where(x => x.Name.ToLower().StartsWith(query.ToLower())
                    && x.Id != ignoreUserId)
                    .Take(searchResultLimit).ToList();
            }
        }

        /// <summary>
        /// Get the requests for the user
        /// </summary>
        public static List<MessengerRequestData> GetRequests(int userId)
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                MessengerRequestData messengerRequestAlias = null;
                PlayerData playerDataAlias = null;

                return session.QueryOver(() => messengerRequestAlias)
                    .JoinQueryOver(() => messengerRequestAlias.FriendData, () => playerDataAlias)
                    .Where(() => messengerRequestAlias.UserId == userId)
                    .List() as List<MessengerRequestData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.MessengerRequestData.Where(x => x.UserId == userId)
                    .Include(x => x.FriendData)
                    .ToList();
            }
        }

        /// <summary>
        /// Get the friends for the user
        /// </summary>
        public static List<MessengerFriendData> GetFriends(int userId)
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                MessengerFriendData messengerFriendAlias = null;
                PlayerData playerDataAlias = null;

                return session.QueryOver(() => messengerFriendAlias)
                    .JoinQueryOver(() => messengerFriendAlias.FriendData, () => playerDataAlias)
                    .Where(() => messengerFriendAlias.UserId == userId)
                    .List() as List<MessengerFriendData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.MessengerFriendData.Where(x => x.UserId == userId)
                    .Include(x => x.FriendData)
                    .ToList();
            }
        }

        /// <summary>
        /// Get the messenger categories for the user
        /// </summary>
        public static List<MessengerCategoryData> GetCategories(int userId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    MessengerCategoryData messengerCategoryAlias = null;

            //    return session.QueryOver(() => messengerCategoryAlias)
            //        .Where(() => messengerCategoryAlias.UserId == userId)
            //        .List() as List<MessengerCategoryData>;
            //}

            using (var context = new StorageContext())
            {
                return context.MessengerCategoryData.Where(x => x.UserId == userId).ToList();
            }
        }

        /// <summary>
        /// Get if the user supports friend requests
        /// </summary>
        public static bool GetAcceptsFriendRequests(int userId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    PlayerSettingsData settingsAlias = null;
            //    PlayerData playerDataAlias = null;

            //    return session.QueryOver(() => settingsAlias)
            //        .JoinEntityAlias(() => playerDataAlias, () => settingsAlias.UserId == playerDataAlias.Id)
            //        .Where(() => playerDataAlias.Id == userId && settingsAlias.FriendRequestsEnabled)
            //        .List().Count > 0;
            //}

            using (var context = new StorageContext())
            {
                return context.MessengerRequestData
                    .Include(x => x.FriendData)
                    .ThenInclude(x => x.Settings)
                    .Any(x => x.FriendData.Settings.FriendRequestsEnabled);
            }

        }

        /// <summary>
        /// Deletes friend requests
        /// </summary>
        public static void DeleteRequests(int userId, int friendId)
        {
            //    using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //    {
            //        session.Query<MessengerRequestData>().Where(x => 
            //            (x.FriendId == friendId && x.UserId == userId) || 
            //            (x.FriendId == userId && x.UserId == friendId))
            //        .Delete();
            //    }

            using (var context = new StorageContext())
            {
                context.MessengerRequestData.RemoveRange(
                    context.MessengerRequestData.Where(x =>
                        (x.FriendId == friendId && x.UserId == userId) || 
                        (x.FriendId == userId && x.UserId == friendId))
                    );
            }
        }

        /// <summary>
        /// Delete all requests by user id
        /// </summary>
        public static void DeleteAllRequests(int userId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<MessengerRequestData>().Where(x =>
            //        (x.FriendId == userId || x.UserId == userId))
            //    .Delete();
            //}

            using (var context = new StorageContext())
            {
                context.MessengerRequestData.RemoveRange(
                    context.MessengerRequestData.Where(x =>
                        (x.FriendId == userId || x.UserId == userId))
                    );
            }
        }

        /// <summary>
        /// Deletes friends
        /// </summary>
        public static void DeleteFriends(int userId, int friendId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<MessengerFriendData>().Where(x =>
            //        (x.FriendId == friendId && x.UserId == userId) ||
            //        (x.FriendId == userId && x.UserId == friendId))
            //    .Delete();
            //}

            using (var context = new StorageContext())
            {
                context.MessengerFriendData.RemoveRange(
                    context.MessengerFriendData.Where(x =>
                       (x.FriendId == friendId && x.UserId == userId) ||
                       (x.FriendId == userId && x.UserId == friendId))
                    );
            }
        }

        /// <summary>
        /// Save a request
        /// </summary>
        public static void SaveRequest(MessengerRequestData messengerRequestData)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            session.Save(messengerRequestData);
            //            transaction.Commit();
            //        }
            //        catch
            //        {
            //            transaction.Rollback();
            //        }
            //    }
            //}

            using (var context = new StorageContext())
            {
                context.MessengerRequestData.Add(messengerRequestData);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Save a request
        /// </summary>
        public static void SaveFriend(MessengerFriendData messengerFriendData)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            session.Save(messengerFriendData);
            //            transaction.Commit();
            //        }
            //        catch
            //        {
            //            transaction.Rollback();
            //        }
            //    }
            //}

            using (var context = new StorageContext())
            {
                context.MessengerFriendData.Add(messengerFriendData);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Save a message
        /// </summary>
        public static void SaveMessage(MessengerChatData messengerChatData)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            session.Save(messengerChatData);
            //            transaction.Commit();
            //        }
            //        catch
            //        {
            //            transaction.Rollback();
            //        }
            //    }
            //}

            using (var context = new StorageContext())
            {
                context.MessengerChatData.Add(messengerChatData);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes friend requests
        /// </summary>
        public static void SetReadMessages(int userId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<MessengerChatData>().Where(x => x.FriendId == userId && !x.IsRead).Update(x => new MessengerChatData { IsRead = true });
            //}

            using (var context = new StorageContext())
            {
                context.MessengerChatData
                    .Where(x => x.FriendId == userId && !x.IsRead)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.IsRead = true;
                    });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes friend requests
        /// </summary>
        public static List<MessengerChatData> GetUneadMessages(int userId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<MessengerChatData>().Where(x => x.FriendId == userId && !x.IsRead).List() as List<MessengerChatData>;
            //}

            using (var context = new StorageContext())
            {
                return context.MessengerChatData.Where(x => x.FriendId == userId && !x.IsRead).ToList();
            }
        }
    }
}
