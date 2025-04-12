using Helios.Messages;
using Helios.Messages.Outgoing;
using Helios.Messages.Outgoing.User.Alerts;
using Helios.Network.Session;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Avatar;
using Helios.Storage.Models.Entity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Helios.Game
{
    public class Avatar : IEntity
    {
        #region Fields

        private AvatarData avatarData;
        private AvatarSettingsData settings;

        #endregion

        #region Interface properties

        /// <summary>
        /// Get room entity
        /// </summary>
        public RoomEntity RoomEntity { get; private set; }

        /// <summary>
        /// Get entity data
        /// </summary>
        public IEntityData EntityData => (IEntityData)avatarData;

        #endregion

        #region Properties

        /// <summary>
        /// Get the connection session
        /// </summary>
        public ConnectionSession Connection { get; private set; }

        /// <summary>
        /// Get messenger
        /// </summary>
        public Messenger Messenger { get; private set; }

        /// <summary>
        /// Get subscription manager
        /// </summary>
        public Subscription Subscription { get; private set; }

        /// <summary>
        /// Get whether has subscription data
        /// </summary>
        public bool IsSubscribed
        {
            get
            {
                if (Subscription.Data == null)
                    return false;

                return Subscription.Data.ExpireDate > DateTime.Now;
            }
        }

        /// <summary>
        /// Get entity data
        /// </summary>
        public AvatarData Details => avatarData;

        /// <summary>
        /// Get avatar settings
        /// </summary>
        public AvatarSettingsData Settings => settings;

        /// <summary>
        /// Get room avatar
        /// </summary>
        public RoomAvatar RoomUser => (RoomAvatar)RoomEntity;

        /// <summary>
        /// Get currency manager for user
        /// </summary>
        public CurrencyManager Currency { get; set; }

        /// <summary>
        /// Get inventory instance for user
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Get effect manager instance for user
        /// </summary>
        public EffectManager EffectManager { get; set; }

        /// <summary>
        /// Whether the avatar has logged in or not
        /// </summary>
        public bool Authenticated { get; private set; }

        /// <summary>
        /// The time when avatar connected
        /// </summary>
        public DateTime AuthenticationTime { get; private set; }

        /// <summary>
        /// Get user group
        /// </summary>
        public UserGroup UserGroup { get { return PermissionsManager.Instance.Ranks[Details.Rank]; } }

        /// <summary>
        /// Session attributes used for various temporarily stored variables
        /// </summary>
        public Dictionary<string, object> LocalStorage { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for avatar.
        /// </summary>
        /// <param name="channel">the channel</param>
        public Avatar(ConnectionSession connectionSession)
        {
            Connection = connectionSession;
            RoomEntity = new RoomAvatar(this);
            LocalStorage = new Dictionary<string, object>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Login handler
        /// </summary>
        /// <param name="ssoTicket">the sso ticket</param>
        /// <returns></returns>
        public bool TryLogin(string ssoTicket)
        {
            using (var context = new StorageContext())
            {
                context.Login(out avatarData, ssoTicket);

                if (avatarData == null)
                    return false;

                Log.ForContext<Avatar>().Debug($"Avatar {avatarData.Name} has logged in");

                context.CreateOrUpdate(out settings, avatarData.Id);

                avatarData.PreviousLastOnline = avatarData.LastOnline;

                avatarData.LastOnline = DateTime.Now;
                context.Update(avatarData);

                avatarData.User.LastOnline = DateTime.Now;
                context.Update(avatarData.User);
            }

            AvatarManager.Instance.AddAvatar(this);

            Subscription = new Subscription(this);
            Subscription.Load();
            Subscription.CountMemberDays();

            Currency = new CurrencyManager(this);
            Currency.Load();

            Inventory = new Inventory(this);
            Inventory.Load();

            Messenger = new Messenger(this);
            Messenger.SendStatus();

            EffectManager = new EffectManager(this);
            EffectManager.Load();

            Authenticated = true;
            AuthenticationTime = DateTime.Now;

            Send(new UserRightsMessageComposer(FuserightManager.Instance.GetRights(avatarData.Rank)));
            Send(new AuthenticationOKComposer());
            Send(new NavigatorSettingsComposer(0));
            Send(new EffectsMessageComposer([.. EffectManager.Effects.Values]));
            Send(new FavouriteRoomsComposer());
            // Send(new AvailabilityStatusComposer());
            // Send(new InfoFeedEnableComposer());
            // Send(new ModMessageComposer("Thank you for helping us test the new Helios. Please submit feedback to the Oldskooler forum:", "https://forum.oldskooler.org/threads/prjhelios-c-r49-entity-framework.27/"));

            return true;
        }

        /// <summary>
        /// Send message composer
        /// </summary>
        public void Send(IMessageComposer composer)
        {
            Connection.Send(composer);
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public void Disconnect()
        {
            if (!Authenticated)
                return;

            if (RoomEntity.Room != null)
                RoomEntity.Room.EntityManager.LeaveRoom(this);

            AvatarManager.Instance.RemoveAvatar(this);

            Messenger.SendStatus();
            Subscription.CountMemberDays();

            using (var context = new StorageContext())
            {
                avatarData.LastOnline = DateTime.Now;
                context.Update(avatarData);

                avatarData.User.LastOnline = DateTime.Now;
                context.Update(avatarData.User);
                // AvatarDao.Update(avatarData);

                long timeInSeconds = (long)(DateTime.Now - AuthenticationTime).TotalSeconds;
                settings.OnlineTime += timeInSeconds;

                context.Update(settings);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
