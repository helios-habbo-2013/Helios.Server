using System;

namespace Helios.Storage.Database.Data
{
    public class AuthenicationTicketData
    {
        public virtual int UserId { get; set; }
        public virtual string Ticket { get; set; }
        public virtual DateTime? ExpireDate { get; set; }

        #region One to Many relationship

        public virtual PlayerData PlayerData { get; set; }

        #endregion
    }
}
