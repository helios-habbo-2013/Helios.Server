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
        public static List<AvatarData> SearchMessenger(string query, int ignoreAvatarId, int searchResultLimit = 30)
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                AvatarData avatarDataAlias = null;

                return session.QueryOver<AvatarData>(() => avatarDataAlias)
                    //.Where(Restrictions.On<AvatarData>(x => x.Name).IsInsensitiveLike(query, MatchMode.Start))
                    .WhereRestrictionOn(() => avatarDataAlias.Name).IsLike(query, MatchMode.Start)
                    .And(() => avatarDataAlias.Id != ignoreAvatarId)
                    .Take(searchResultLimit)
                    .List() as List<AvatarData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.AvatarData.Where(x => x.Name.ToLower().StartsWith(query.ToLower())
                    && x.Id != ignoreAvatarId)
                    .Take(searchResultLimit).ToList();
            }
        }

        /// <summary>
        /// Get the requests for the user
        /// </summary>
        public static List<MessengerRequestData> GetRequests(int AvatarId)
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                MessengerRequestData messengerRequestAlias = null;
                AvatarData avatarDataAlias = null;

                return session.QueryOver(() => messengerRequestAlias)
                    .JoinQueryOver(() => messengerRequestAlias.FriendData, () => avatarDataAlias)
                    .Where(() => messengerRequestAlias.AvatarId == AvatarId)
                    .List() as List<MessengerRequestData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.MessengerRequestData.Where(x => x.AvatarId == AvatarId)
                    .Include(x => x.FriendData)
                    .ToList();
            }
        }

        /// <summary>
        /// Get the friends for the user
        /// </summary>
        public static List<MessengerFriendData> GetFriends(int AvatarId)
        {
            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                MessengerFriendData messengerFriendAlias = null;
                AvatarData avatarDataAlias = null;

                return session.QueryOver(() => messengerFriendAlias)
                    .JoinQueryOver(() => messengerFriendAlias.FriendData, () => avatarDataAlias)
                    .Where(() => messengerFriendAlias.AvatarId == AvatarId)
                    .List() as List<MessengerFriendData>;
            }*/

            using (var context = new StorageContext())
            {
                return context.MessengerFriendData.Where(x => x.AvatarId == AvatarId)
                    .Include(x => x.FriendData)
                    .ToList();
            }
        }

        /// <summary>
        /// Get the messenger categories for the user
        /// </summary>
        public static List<MessengerCategoryData> GetCategories(int AvatarId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    MessengerCategoryData messengerCategoryAlias = null;

            //    return session.QueryOver(() => messengerCategoryAlias)
            //        .Where(() => messengerCategoryAlias.AvatarId == AvatarId)
            //        .List() as List<MessengerCategoryData>;
            //}

            using (var context = new StorageContext())
            {
                return context.MessengerCategoryData.Where(x => x.AvatarId == AvatarId).ToList();
            }
        }

        /// <summary>
        /// Get if the user supports friend requests
        /// </summary>
        public static bool GetAcceptsFriendRequests(int AvatarId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    AvatarSettingsData settingsAlias = null;
            //    AvatarData avatarDataAlias = null;

            //    return session.QueryOver(() => settingsAlias)
            //        .JoinEntityAlias(() => avatarDataAlias, () => settingsAlias.AvatarId == avatarDataAlias.Id)
            //        .Where(() => avatarDataAlias.Id == AvatarId && settingsAlias.FriendRequestsEnabled)
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
        public static void DeleteRequests(int AvatarId, int friendId)
        {
            //    using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //    {
            //        session.Query<MessengerRequestData>().Where(x => 
            //            (x.FriendId == friendId && x.AvatarId == AvatarId) || 
            //            (x.FriendId == AvatarId && x.AvatarId == friendId))
            //        .Delete();
            //    }

            using (var context = new StorageContext())
            {
                context.MessengerRequestData.RemoveRange(
                    context.MessengerRequestData.Where(x =>
                        (x.FriendId == friendId && x.AvatarId == AvatarId) || 
                        (x.FriendId == AvatarId && x.AvatarId == friendId))
                    );
            }
        }

        /// <summary>
        /// Delete all requests by user id
        /// </summary>
        public static void DeleteAllRequests(int AvatarId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<MessengerRequestData>().Where(x =>
            //        (x.FriendId == AvatarId || x.AvatarId == AvatarId))
            //    .Delete();
            //}

            using (var context = new StorageContext())
            {
                context.MessengerRequestData.RemoveRange(
                    context.MessengerRequestData.Where(x =>
                        (x.FriendId == AvatarId || x.AvatarId == AvatarId))
                    );
            }
        }

        /// <summary>
        /// Deletes friends
        /// </summary>
        public static void DeleteFriends(int AvatarId, int friendId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<MessengerFriendData>().Where(x =>
            //        (x.FriendId == friendId && x.AvatarId == AvatarId) ||
            //        (x.FriendId == AvatarId && x.AvatarId == friendId))
            //    .Delete();
            //}

            using (var context = new StorageContext())
            {
                context.MessengerFriendData.RemoveRange(
                    context.MessengerFriendData.Where(x =>
                       (x.FriendId == friendId && x.AvatarId == AvatarId) ||
                       (x.FriendId == AvatarId && x.AvatarId == friendId))
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
        public static void SetReadMessages(int AvatarId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    session.Query<MessengerChatData>().Where(x => x.FriendId == AvatarId && !x.IsRead).Update(x => new MessengerChatData { IsRead = true });
            //}

            using (var context = new StorageContext())
            {
                context.MessengerChatData
                    .Where(x => x.FriendId == AvatarId && !x.IsRead)
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
        public static List<MessengerChatData> GetUneadMessages(int AvatarId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<MessengerChatData>().Where(x => x.FriendId == AvatarId && !x.IsRead).List() as List<MessengerChatData>;
            //}

            using (var context = new StorageContext())
            {
                return context.MessengerChatData.Where(x => x.FriendId == AvatarId && !x.IsRead).ToList();
            }
        }
    }
}
