using Helios.Storage.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Database.Access
{
    public class RoomDao
    {
        public static List<RoomData> SearchRooms(string query, int roomLimit = 50)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    RoomData roomDataAlias = null;
            //    PlayerData playerDataAlias = null;
            //    return session.QueryOver<RoomData>(() => roomDataAlias)
            //        .JoinQueryOver<PlayerData>(() => roomDataAlias.OwnerData, () => playerDataAlias)
            //            .Where(() => playerDataAlias.Id == roomDataAlias.OwnerId)
            //        .Where(
            //            Restrictions.On(() => roomDataAlias.Name).IsInsensitiveLike(query, MatchMode.Anywhere) ||
            //            Restrictions.On(() => playerDataAlias.Name).IsInsensitiveLike(query, MatchMode.Anywhere))
            //        .OrderBy(() => roomDataAlias.UsersNow).Desc
            //        .OrderBy(() => roomDataAlias.Rating).Desc
            //        .Take(roomLimit)
            //        .List<RoomData>() as List<RoomData>;
            //}

            using (var context = new StorageContext())
            {
                return context.RoomData
                    .Include(x => x.OwnerData)
                    .Include(x => x.Category)
                    .Include(x => x.Tags)
                    .Where(x =>
                        x.Name.ToLower().Contains(query.ToLower()) ||
                        x.OwnerData.Name.ToLower().Contains(query.ToLower()))
                    .OrderByDescending(x => x.UsersNow)
                    .OrderByDescending(x => x.Rating)
                    .Take(roomLimit)
                    .ToList();
            }

        }

        /// <summary>
        /// Search rooms by tag
        /// </summary>
        public static List<RoomData> SearchTags(string tag, int roomLimit = 50)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    RoomData roomDataAlias = null;
            //    TagData tagAlias = null;
            //    PlayerData ownerAlias = null;

            //    return session.QueryOver<RoomData>(() => roomDataAlias)
            //        .JoinQueryOver<TagData>(() => roomDataAlias.Tags, () => tagAlias)
            //        .JoinQueryOver<PlayerData>(() => roomDataAlias.OwnerData, () => ownerAlias)
            //        .Where(() => 
            //            tagAlias.Text == tag &&
            //            ownerAlias.Name != null)
            //        .OrderBy(() => roomDataAlias.UsersNow).Desc
            //        .OrderBy(() => roomDataAlias.Rating).Desc
            //        .Take(roomLimit)
            //        .List<RoomData>() as List<RoomData>;
            //}

            using (var context = new StorageContext())
            {
                return context.RoomData
                    .Include(x => x.OwnerData)
                    .Include(x => x.Tags)
                    .Include(x => x.Category)
                    .Where(x =>
                        x.Tags.Any(t => t.Text == tag))
                    .OrderByDescending(x => x.UsersNow)
                    .OrderByDescending(x => x.Rating)
                    .Take(roomLimit)
                    .ToList();
            }

        }

        /// <summary>
        /// Get the list of users' own rooms
        /// </summary>
        public static List<RoomData> GetPopularFlats(int resultsLimit = 50)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    RoomData roomDataAlias = null;
            //    PlayerData playerDataAlias = null;

            //    return session.QueryOver<RoomData>(() => roomDataAlias)
            //        .JoinQueryOver<PlayerData>(() => roomDataAlias.OwnerData, () => playerDataAlias)
            //            .Where(() => playerDataAlias.Id == roomDataAlias.OwnerId)
            //        .OrderBy(() => roomDataAlias.UsersNow).Desc
            //        .OrderBy(() => roomDataAlias.Rating).Desc
            //        .Take(resultsLimit)
            //        .List() as List<RoomData>;
            //}

            using (var context = new StorageContext())
            {
                return context.RoomData
                    .Include(x => x.OwnerData)
                    .Include(x => x.Category)
                    .Include(x => x.Tags)
                    .Where(x => x.OwnerId > 0)
                    .OrderByDescending(x => x.UsersNow)
                    .OrderByDescending(x => x.Rating)
                    .Take(resultsLimit)
                    .ToList();
            }

        }

        /// <summary>
        /// Get the list of users' own rooms
        /// </summary>
        public static List<RoomData> GetUserRooms(int userId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    RoomData roomDataAlias = null;

            //    return session.QueryOver<RoomData>(() => roomDataAlias)
            //        .Where(() => roomDataAlias.OwnerId == userId)
            //        .OrderBy(() => roomDataAlias.UsersNow).Desc
            //        .OrderBy(() => roomDataAlias.Rating).Desc
            //        .List() as List<RoomData>;
            //}

            using (var context = new StorageContext())
            {
                return context.RoomData
                    .Include(x => x.OwnerData)
                    .Include(x => x.Category)
                    .Include(x => x.Tags)
                    .Where(x => x.OwnerId == userId)
                    .OrderByDescending(x => x.UsersNow)
                    .OrderByDescending(x => x.Rating)
                    .ToList();
            }

        }

        /// <summary>
        /// Get the list of users' own rooms
        /// </summary>
        public static List<TagData> GetRoomTags(int roomId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    TagData tagDataAlias = null;
            //    return session.QueryOver<TagData>(() => tagDataAlias).Where(() => tagDataAlias.RoomId == roomId).List() as List<TagData>;
            //}

            using (var context = new StorageContext())
            {
                return context.TagData.Where(x => x.RoomId == roomId).ToList();
            }
        }

        /// <summary>
        /// Count the rooms the user has.
        /// </summary>
        public static int CountUserRooms(int userId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<RoomData>().Where(x => x.OwnerId == userId).RowCount();
            //}

            using (var context = new StorageContext())
            {
                return context.RoomData.Count(x => x.OwnerId == userId);
            }
        }

        /// <summary>
        /// Get the room model data
        /// </summary>
        /// <returns></returns>
        public static List<RoomModelData> GetModels()
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<RoomModelData>().List() as List<RoomModelData>;
            //}
            using (var context = new StorageContext())
            {
                return context.RoomModelData.ToList();
            }
        }

        /// <summary>
        /// Get data for room
        /// </summary>
        public static RoomData GetRoomData(int roomId)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<RoomData>().Where(x => x.Id == roomId).Take(1).SingleOrDefault();
            //}
            using (var context = new StorageContext())
            {
                return context.RoomData
                    .Include(x => x.OwnerData)
                    .Include(x => x.Category)
                    .Include(x => x.Tags)
                    .FirstOrDefault(x => x.Id == roomId);
            }

        }

        /// <summary>
        /// Save room data
        /// </summary>
        public static void SaveRoom(RoomData data)
        {
            using (var context = new StorageContext())
            {
                context.RoomData.Update(data);
                context.SaveChanges();
            }
                //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                //{
                //    using (var transaction = session.BeginTransaction())
                //    {
                //        try
                //        {
                //            session.Update(data);
                //            transaction.Commit();
                //            session.Refresh(data);
                //        }
                //        catch
                //        {
                //            transaction.Rollback();
                //        }
                //    }
                //}
            }

        /// <summary>
        /// New room data
        /// </summary>
        public static void NewRoom(RoomData data)
        {

            using (var context = new StorageContext())
            {
                context.RoomData.Add(data);
                context.SaveChanges();
            }

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            session.Save(data);
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
        /// Reset all visitors
        /// </summary>
        public static void ResetVisitorCounts()
        {
            using (var context = new StorageContext())
            {
                context.RoomData.Where(x => x.UsersNow > 0).UpdateFromQuery(x => new RoomData { UsersNow = 0 });
            }
                //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                //{
                //    session.Query<RoomData>().Where(x => x.UsersNow > 0 || x.UsersNow < 0).Update(x => new RoomData { UsersNow = 0 });
                //}
            }

        /// <summary>
        /// Update users count
        /// </summary>
        public static void SetVisitorCount(int roomId, int visitorsNow)
        {
            using (var context = new StorageContext())
            {

                context.RoomData.Where(x => x.Id == roomId).UpdateFromQuery(x => new RoomData { UsersNow = visitorsNow });
                //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                //{
                //    session.Query<RoomData>().Where(x => x.Id == roomId).Update(x => new RoomData { UsersNow = visitorsNow });
                //}
            }
        }
    }
}
