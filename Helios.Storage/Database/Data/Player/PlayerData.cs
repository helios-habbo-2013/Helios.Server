using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helios.Storage.Database.Data
{
    public class PlayerData : IEntityData
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual string Figure { get; set; }

        public virtual string Sex { get; set; }
        public virtual int Rank { get; set; }

        public virtual int Credits { get; set; }

        public virtual DateTime JoinDate { get; set; }

        public virtual DateTime LastOnline { get; set; }

        public virtual DateTime PreviousLastOnline { get; set; }

        public virtual string Motto { get; set; }

        public virtual string RealName => string.Empty;
        public virtual int AchievementPoints => 0;

        #region Contraints

        public virtual List<ItemData> Items { get; set; }
        public virtual List<RoomData> Rooms { get; set; }
        public virtual List<AuthenicationTicketData> Tickets { get; set; }
        public virtual List<MessengerFriendData> Friends { get; set; }
        public virtual List<MessengerRequestData> Requests { get; set; }
        public virtual PlayerSettingsData Settings { get; set; }

        #endregion
    }
}
