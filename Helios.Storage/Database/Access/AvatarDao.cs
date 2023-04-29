using Helios.Storage.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq;

namespace Helios.Storage.Database.Access
{
    public class AvatarDao
    {
        /// <summary>
        /// Login user with SSO ticket
        /// </summary>
        public static bool Login(out AvatarData loginData, string ssoTicket)
        {
            AvatarData avatarData = null;

            using (var context = new StorageContext())
            {
                var row = context.AuthenicationTicketData.Include(x => x.AvatarData).Where(x =>
                       (x.AvatarData != null && x.Ticket == ssoTicket) &&
                       (x.ExpireDate == null || x.ExpireDate > DateTime.Now))
                   .Take(1)
                   .SingleOrDefault();

                if (row != null)
                    avatarData = row.AvatarData;
            }

            loginData = avatarData;
            return false;

            //AvatarData avatarData = null;
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    AuthenicationTicketData ticketAlias = null;
            //    AvatarData avatarDataAlias = null;
            //    var row = session.QueryOver(() => ticketAlias)
            //        .JoinQueryOver(() => ticketAlias.AvatarData, () => avatarDataAlias)
            //        .Where(() =>
            //            (ticketAlias.AvatarData != null && ticketAlias.Ticket == ssoTicket) &&
            //            (ticketAlias.AvatarId == avatarDataAlias.Id) &&
            //            (ticketAlias.ExpireDate == null || ticketAlias.ExpireDate > DateTime.Now))
            //        .Take(1)
            //    .SingleOrDefault();

            //    if (row != null)
            //        avatarData = row.AvatarData;
            //}
            //loginData = avatarData;
            // return false;
        }

        /// <summary>
        /// Save avatar data
        /// </summary>
        /// <param name="avatarData">the avatar data to save</param>
        public static void Update(AvatarData avatarData)
        {
            using (var context = new StorageContext())
            {
                context.Attach(avatarData).Property(x => x.Credits).IsModified = false; // don't override credits amount
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get avatar by username
        /// </summary>
        public static AvatarData GetByName(string name)
        {
            using (var context = new StorageContext())
            {
                return context.AvatarData.FirstOrDefault(x => x.Name == name);
            }
        }

        /// <summary>
        /// Get avatar by id
        /// </summary>
        public static AvatarData GetById(int id)
        {
            using (var context = new StorageContext())
            {
                return context.AvatarData.FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Get avatar name by id
        /// </summary>
        public static string GetNameById(int id)
        {
            using (var context = new StorageContext())
            {
                return context.AvatarData.Where(x => x.Id == id).Select(x => x.Name).SingleOrDefault();
            }
        }

        /// <summary>
        /// Get avatar id by name
        /// </summary>
        public static int GetIdByName(string name)
        {
            using (var context = new StorageContext())
            {
                return context.AvatarData.Where(x => x.Name == name).Select(x => x.Id).SingleOrDefault();
            }
        }
    }
}
