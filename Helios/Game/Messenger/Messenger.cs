using Helios.Messages.Outgoing;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Messenger;
using Helios.Storage.Models.Subscription;
using Helios.Util.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Game
{
    public class Messenger
    {
        #region Fields

        private SubscriptionData subscription;

        #endregion

        #region Properties

        /// <summary>
        /// Get friend requests
        /// </summary>
        public List<MessengerUser> Requests { get; private set; }

        /// <summary>
        /// Get friends
        /// </summary>
        public List<MessengerUser> Friends { get; private set; }

        /// <summary>
        /// Get categories
        /// </summary>
        public List<MessengerCategoryData> Categories { get; private set; }

        /// <summary>
        /// Get concurrent messenger update queue
        /// </summary>
        public ConcurrentQueue<MessengerUpdate> Queue { get; private set; }

        /// <summary>
        /// Get the maximum friends allowed
        /// </summary>
        public int MaxFriendsAllowed
        {
            get
            {
                if (subscription == null)
                    return ValueManager.Instance.GetInt("max.friends.normal");

                /*return m_Subscription.Type == SubscriptionType.HC ?
                    ValueManager.Instance.GetInt("max.friends.hc") :
                    ValueManager.Instance.GetInt("max.friends.vip");*/

                return ValueManager.Instance.GetInt("max.friends.hc");
            }
        }

        /// <summary>
        /// Get the avatar for this messenger instance
        /// </summary>
        public Avatar Avatar { get; set; }

        /// <summary>
        /// Get whether friend requests are enabled
        /// </summary>
        public bool FriendRequestsEnabled { get; set; }

        /// <summary>
        /// Get the avatar as messenger user
        /// </summary>
        public MessengerUser MessengerUser => new MessengerUser (Avatar.Details);

        #endregion

        #region Constructors

        public Messenger(Avatar avatar)
        {
            Avatar = avatar;
            subscription = avatar.Subscription.Data;
            FriendRequestsEnabled = avatar.Settings.FriendRequestsEnabled;
            LoadMessengerData(avatar.Details.Id);
        }

        public Messenger(int AvatarId)
        {
            using (var context = new StorageContext())
            {
                subscription = context.GetSubscription(AvatarId);
                FriendRequestsEnabled = context.GetAcceptsFriendRequests(AvatarId);
            }

            LoadMessengerData(AvatarId);
        }

        #endregion

        #region Static methods

        public static Messenger GetMessengerData(int AvatarId)
        {
            var avatar = AvatarManager.Instance.GetAvatarById(AvatarId);

            if (avatar != null)
                return avatar.Messenger;

            return new Messenger(AvatarId);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load messenger data by given useer id
        /// </summary>
        private void LoadMessengerData(int AvatarId)
        {
            using (var context = new StorageContext())
            {
                Friends = context.GetFriends(AvatarId).Select(data => new MessengerUser(data.FriendData)).ToList();
                Requests = context.GetRequests(AvatarId).Select(data => new MessengerUser(data.FriendData)).ToList();
                Categories = context.GetCategories(AvatarId);
            }

            Queue = new ConcurrentQueue<MessengerUpdate>();
        }

        /// <summary>
        /// Queue specific messenger update
        /// </summary>
        /// <param name="updateType">the update type</param>
        /// <param name="messengerUser">the messenger user</param>
        public void QueueUpdate(MessengerUpdateType updateType, MessengerUser messengerUser)
        {
            Queue.Enqueue(new MessengerUpdate
            {
                UpdateType = updateType,
                Friend = messengerUser
            });
        }

        /// <summary>
        /// Send status update to all friends
        /// </summary>
        public void SendStatus()
        {
            var onlineFriends = GetOnlineFriends();

            foreach (var friend in onlineFriends)
                friend.Avatar.Messenger.QueueUpdate(MessengerUpdateType.UpdateFriend, MessengerUser);

            foreach (var friend in onlineFriends)
                friend.Avatar.Messenger.ForceUpdate();
        }

        /// <summary>
        /// Forces update to own messenger
        /// </summary>
        public void ForceUpdate()
        {
            List<MessengerUpdate> messengerUpdates = Queue.Dequeue();

            if (messengerUpdates.Count > 0)
                Avatar.Send(new FriendListUpdateComposer(Categories, messengerUpdates));
        }

        /// <summary>
        /// Remove friend by user id
        /// </summary>
        public void RemoveFriend(int id)
        {
            Friends.RemoveAll(x => x.AvatarData.Id == id);
        }

        /// <summary>
        /// Remove request by user id
        /// </summary>
        public void RemoveRequest(int id)
        {
            Requests.RemoveAll(x => x.AvatarData.Id == id);
        }

        /// <summary>
        /// Get the list of all online friends
        /// </summary>
        public List<MessengerUser> GetOnlineFriends() => 
            Friends.Where(friend => friend.IsOnline).ToList();
             
        public bool HasFriend(int AvatarId) => 
            Friends.Count(friend => friend.AvatarData.Id == AvatarId) > 0;

        public MessengerUser GetFriend(int AvatarId) =>
            Friends.FirstOrDefault(friend => friend.AvatarData.Id == AvatarId);

        public bool HasRequest(int AvatarId) =>
            Requests.Count(requester => requester.AvatarData.Id == AvatarId) > 0;

        public MessengerUser GetRequest(int AvatarId) =>
            Requests.FirstOrDefault(friend => friend.AvatarData.Id == AvatarId);

        #endregion
    }
}
