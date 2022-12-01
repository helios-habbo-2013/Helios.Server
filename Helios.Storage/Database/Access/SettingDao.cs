using System.Collections.Generic;
using System.Linq;
using Helios.Storage.Database.Data;

namespace Helios.Storage.Database.Access
{
    public class SettingDao
    {
        public static void GetSettings(out Dictionary<string, string> settings)
        {
            using (var context = new StorageContext())
            {
                settings = context.SettingsData.ToList().ToDictionary(x => x.Key, x => x.Value);
            }
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    settings = session.QueryOver<SettingsData>().List().ToDictionary(x => x.Key, x => x.Value);
            //}
        }

        public static bool HasSetting(string key)
        {
            using (var context = new StorageContext())
            {
                return context.SettingsData.Count(x => x.Key == key) > 0;
            }
        }

        public static void SaveSetting(string key, string value)
        {
            using (var context = new StorageContext())
            {
                var setting = context.SettingsData.FirstOrDefault(x => x.Key == key);

                if (setting == null)
                {
                    setting = new SettingsData()
                    {
                        Key = key,
                        Value = value
                    };

                    context.SettingsData.Add(setting);
                }
                else
                {
                    setting.Value = value;
                    context.Update(setting);
                }

                context.SaveChanges();
            }

            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        try
            //        {
            //            session.Save(new SettingsData { Key = key, Value = value });
            //            transaction.Commit();
            //        }
            //        catch
            //        {
            //            transaction.Rollback();
            //        }
            //    }
            //}
        }
    }
}
