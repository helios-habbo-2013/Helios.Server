using Helios.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Database.Access
{
    public class CurrencyDao
    {
        /// <summary>
        /// Get currency data for user, if doesn't exist, create rows in database for each currency
        /// </summary>
        public static List<CurrencyData> GetCurrencies(int userId)
        {
            List<CurrencyData> currencyList = new List<CurrencyData>();

            using (var context = new StorageContext())
            {
                currencyList = context.CurrencyData.Where(x => x.UserId == userId).ToList();

                if (!currencyList.Any())
                {
                    currencyList = new List<CurrencyData>();
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.PUMPKINS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.PEANUTS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.STARS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.CLOUDS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.DIAMONDS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.DUCKETS, Balance = 0 });
                    currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.LOYALTY_POINTS, Balance = 0 });

                    context.AddRange(currencyList);
                    context.SaveChanges();
                }
            }

                /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                {
                    CurrencyData currencyDataAlias = null;

                    currencyList = session.QueryOver(() => currencyDataAlias)
                        .Where(() => currencyDataAlias.UserId == userId)
                        .List() as List<CurrencyData>;

                    if (!currencyList.Any())
                    {
                        currencyList = new List<CurrencyData>();
                        currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.PUMPKINS, Balance = 0 });
                        currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.PEANUTS, Balance = 0 });
                        currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.STARS, Balance = 0 });
                        currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.CLOUDS, Balance = 0 });
                        currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.DIAMONDS, Balance = 0 });
                        currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.DUCKETS, Balance = 0 });
                        currencyList.Add(new CurrencyData { UserId = userId, SeasonalType = SeasonalCurrencyType.LOYALTY_POINTS, Balance = 0 });

                        using (var transaction = session.BeginTransaction())
                        {
                            try
                            {
                                foreach (var currencyData in currencyList)
                                    session.Save(currencyData);

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                transaction.Rollback();

                            }

                        }
                    }
                }*/

                return currencyList;
        }

        /// <summary>
        /// Get singular currency for user straight from database
        /// </summary>
        public static CurrencyData GetCurrency(int userId, SeasonalCurrencyType seasonalCurrencyType)
        {
            using (var context = new StorageContext())
            {
                return context.CurrencyData.SingleOrDefault(x => x.UserId == userId && x.SeasonalType == seasonalCurrencyType);

            }
                /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                {
                    CurrencyData currencyDataAlias = null;

                    return session.QueryOver(() => currencyDataAlias).Where(() => currencyDataAlias.UserId == userId && currencyDataAlias.SeasonalType == seasonalCurrencyType).SingleOrDefault();
                }*/
        }

        /// <summary>
        /// Save all currencies for user
        /// </summary>
        public static void SaveCurrencies(List<CurrencyData> currencyList)
        {
            using (var context = new StorageContext())
            {
                context.CurrencyData.UpdateRange(currencyList);
                context.SaveChanges();

            }

            /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var currencyData in currencyList)
                            session.Update(currencyData);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }*/
        }

        /// <summary>
        /// Retrives adjusts the credits to change from database and saves it again, then returns it
        /// </summary>
        public static int SaveCredits(int userId, int creditsChanged)
        {
            using (var context = new StorageContext())
            {
                var playerData = context.PlayerData.FirstOrDefault(x => x.Id == userId);
                playerData.Credits += creditsChanged;

                context.PlayerData.Update(playerData);
                context.SaveChanges();

                return context.PlayerData.FirstOrDefault(x => x.Id == userId).Credits;
                /*using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
                {
                    session.Query<PlayerData>().Where(x => x.Id == userId).Update(x => new PlayerData { Credits = x.Credits + creditsChanged });
                    return session.QueryOver<PlayerData>().Where(x => x.Id == userId).Select(x => x.Credits).SingleOrDefault<int>();
                }*/

            }
        }
    }
}
