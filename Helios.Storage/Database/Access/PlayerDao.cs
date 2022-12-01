using Helios.Storage.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace Helios.Storage.Database.Access
{
    public class PlayerDao
    {
        /// <summary>
        /// Login user with SSO ticket
        /// </summary>
        public static bool Login(out PlayerData loginData, string ssoTicket)
        {
            PlayerData playerData = null;

            using (var context = new StorageContext())
            {
                var row = context.AuthenicationTicketData.Include(x => x.PlayerData).Where(x =>
                       (x.PlayerData != null && x.Ticket == ssoTicket) &&
                       (x.ExpireDate == null || x.ExpireDate > DateTime.Now))
                   .Take(1)
                   .SingleOrDefault();

                if (row != null)
                    playerData = row.PlayerData;
            }

            loginData = playerData;
            return false;

            //PlayerData playerData = null;
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    AuthenicationTicketData ticketAlias = null;
            //    PlayerData playerDataAlias = null;
            //    var row = session.QueryOver(() => ticketAlias)
            //        .JoinQueryOver(() => ticketAlias.PlayerData, () => playerDataAlias)
            //        .Where(() =>
            //            (ticketAlias.PlayerData != null && ticketAlias.Ticket == ssoTicket) &&
            //            (ticketAlias.UserId == playerDataAlias.Id) &&
            //            (ticketAlias.ExpireDate == null || ticketAlias.ExpireDate > DateTime.Now))
            //        .Take(1)
            //    .SingleOrDefault();

            //    if (row != null)
            //        playerData = row.PlayerData;
            //}
            //loginData = playerData;
            // return false;
        }

        /// <summary>
        /// Save player data
        /// </summary>
        /// <param name="playerData">the player data to save</param>
        public static void Update(PlayerData playerData)
        {
            using (var context = new StorageContext())
            {
                context.Attach(playerData).Property(x => x.Credits).IsModified = false; // don't override credits amount
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get player by username
        /// </summary>
        public static PlayerData GetByName(string name)
        {
            using (var context = new StorageContext())
            {
                return context.PlayerData.FirstOrDefault(x => x.Name == name);
            }
        }

        /// <summary>
        /// Get player by id
        /// </summary>
        public static PlayerData GetById(int id)
        {
            using (var context = new StorageContext())
            {
                return context.PlayerData.FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Get player name by id
        /// </summary>
        public static string GetNameById(int id)
        {
            using (var context = new StorageContext())
            {
                return context.PlayerData.Where(x => x.Id == id).Select(x => x.Name).SingleOrDefault();
            }
        }

        /// <summary>
        /// Get player id by name
        /// </summary>
        public static int GetIdByName(string name)
        {
            using (var context = new StorageContext())
            {
                return context.PlayerData.Where(x => x.Name == name).Select(x => x.Id).SingleOrDefault();
            }
        }
    }
}
