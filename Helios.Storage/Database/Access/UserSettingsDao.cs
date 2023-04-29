using System.Linq;
using Helios.Storage.Database.Data;
using MySqlX.XDevAPI;

namespace Helios.Storage.Database.Access
{
    public class UserSettingsDao
    {
        /// <summary>
        /// Create avatar statistics
        /// </summary>
        public static void CreateOrUpdate(out AvatarSettingsData settingsData, int AvatarId)
        {
            settingsData = new AvatarSettingsData
            {
                AvatarId = AvatarId
            };

            using (var context = new StorageContext())
            {
                if (!context.AvatarSettingsData.Any(x => x.AvatarId == AvatarId))
                {
                    context.AvatarSettingsData.Add(settingsData);
                    context.SaveChanges();
                } 
                else
                {
                    settingsData = context.AvatarSettingsData.SingleOrDefault(x => x.AvatarId == AvatarId);
                }
            }

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    bool rowExists = session.QueryOver<AvatarSettingsData>().Where(x => x.AvatarId == AvatarId).RowCount() > 0;

            //    if (!rowExists)
            //    {
            //        using (var transaction = session.BeginTransaction())
            //        {
            //            try
            //            {
            //                session.SaveOrUpdate(settingsData);
            //                transaction.Commit();
            //            }
            //            catch
            //            {
            //                transaction.Rollback();
            //            }
            //        }
            //    } 
            //    else
            //    {
            //        settingsData = session.QueryOver<AvatarSettingsData>().Where(x => x.AvatarId == AvatarId).SingleOrDefault();
            //    }
            //}
        }

        /// <summary>
        /// Save avatar statistics
        /// </summary>
        public static void Update(AvatarSettingsData settingsData)
        {
            using (var context = new StorageContext())
            {
                context.AvatarSettingsData.Update(settingsData);
                context.SaveChanges();
            }
        }
    }
}
