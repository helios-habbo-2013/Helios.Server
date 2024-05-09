using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Avatar;
using Helios.Util.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Game
{
    public class AvatarManager
    {
        #region Fields

        public static readonly AvatarManager Instance = new AvatarManager();

        #endregion

        #region Properties

        /// <summary>
        /// Get dictionary of avatars with id's as keys
        /// </summary>
        public ConcurrentDictionary<int, Avatar> AvatarIds { get; private set; }

        /// <summary>
        /// Get dictionary of avatars with names as keys
        /// </summary>
        public ConcurrentDictionary<string, Avatar> AvatarNames { get; private set; }

        /// <summary>
        /// Get the list of online avatars
        /// </summary>
        public List<Avatar> Avatars
        {
            get => AvatarIds.Values.ToList();
        }

        #endregion

        #region Constructors

        public AvatarManager()
        {
            AvatarIds = new ConcurrentDictionary<int, Avatar>();
            AvatarNames = new ConcurrentDictionary<string, Avatar>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add the avatar
        /// </summary>
        /// <param name="avatar">remove the avatar</param>
        public void AddAvatar(Avatar avatar)
        {
            AvatarIds.TryAdd(avatar.EntityData.Id, avatar);
            AvatarNames.TryAdd(avatar.EntityData.Name.ToLower(), avatar);
        }

        /// <summary>
        /// Add the avatar
        /// </summary>
        /// <param name="avatar">remove the avatar</param>
        public void RemoveAvatar(Avatar avatar)
        {
            AvatarIds.Remove(avatar.EntityData.Id);
            AvatarNames.Remove(avatar.EntityData.Name.ToLower());
        }

        /// <summary>
        /// Get the avatar by username
        /// </summary>
        /// <param name="username">the avatar username</param>
        /// <returns></returns>
        public Avatar GetAvatarByName(string username)
        {
            return AvatarNames.TryGetValue(username.ToLower(), out var avatar) ? avatar : null;
        }

        /// <summary>
        /// Get the avatar by id
        /// </summary>
        /// <param name="id">the avatar username</param>
        /// <returns></returns>
        public Avatar GetAvatarById(int id)
        {
            return AvatarIds.TryGetValue(id, out var avatar) ? avatar : null;
        }

        /// <summary>
        /// Get data by id
        /// </summary>
        public AvatarData GetDataById(int AvatarId)
        {
            var avatar = GetAvatarById(AvatarId);

            if (avatar != null)
                return avatar.Details;

            using (var context = new GameStorageContext())
            {
                return context.GetAvatarById(AvatarId);
            }
        }

        /// <summary>
        /// Get data by name
        /// </summary>
        public AvatarData GetDataByName(int AvatarId)
        {
            var avatar = GetAvatarById(AvatarId);

            if (avatar != null)
                return avatar.Details;

            using (var context = new GameStorageContext())
            {
                return context.GetAvatarById(AvatarId);
            }
        }

        /// <summary>
        /// Get data by name
        /// </summary>
        public string GetName(int AvatarId)
        {
            var avatar = GetAvatarById(AvatarId);

            if (avatar != null)
                return avatar.Details.Name;

            using (var context = new GameStorageContext())
            {
                return context.GetNameById(AvatarId);
            }
        }

        #endregion
    }
}
