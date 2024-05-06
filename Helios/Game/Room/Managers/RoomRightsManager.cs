using Helios.Messages.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Util.Extensions;
using System.Threading.Tasks;
using Helios.Storage.Access;
using Helios.Storage.Models.Room;

namespace Helios.Game
{
    public class RoomRightsManager
    {
        #region Fields

        private Room room;
        private List<int> rights;

        #endregion

        #region Constructors

        public RoomRightsManager(Room room)
        {
            this.room = room;
            this.rights = RoomDao.GetRoomRights(room.Data.Id).Select(x => x.AvatarId).ToList();
        }

        #endregion

        #region Public methods


        /// <summary>
        /// Get if the user has rights
        /// </summary>
        public bool HasRights(int AvatarId)
        {
            if (room.Data.OwnerId == AvatarId)
                return true;

            if (rights.Contains(AvatarId))
                return true;

            var avatar = AvatarManager.Instance.GetAvatarById(AvatarId);

            if (avatar != null)
            {
                if (avatar.UserGroup.HasPermission("room.rights"))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Get if the user is owner
        /// </summary>
        public bool IsOwner(int AvatarId)
        {
            if (room.Data.OwnerId == AvatarId)
                return true;

            var avatar = AvatarManager.Instance.GetAvatarById(AvatarId);

            if (avatar != null)
            {
                if (avatar.UserGroup.HasPermission("room.owner"))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Give rights to avatar
        /// </summary>
        /// <param name="id"></param>
        public void AddRights(int avatarId)
        {
            var playerEntity = AvatarManager.Instance.GetAvatarById(avatarId);

            RoomDao.AddRights(room.Data.Id, avatarId);

            rights.Add(avatarId);

            if (playerEntity != null)
            {
                playerEntity.RoomUser.AddStatus("flatctrl", "1");
                playerEntity.RoomUser.NeedsUpdate = true;

                playerEntity.Send(new YouAreControllerComposer(1));
            }
        }

        /// <summary>
        /// Remove rights from avatar
        /// </summary>
        /// <param name="id"></param>
        public void RemoveRights(int avatarId)
        {
            var playerEntity = AvatarManager.Instance.GetAvatarById(avatarId);

            RoomDao.RemoveRights(room.Data.Id, avatarId);

            rights.Remove(avatarId);

            if (playerEntity != null)
            {
                playerEntity.RoomUser.AddStatus("flatctrl", "0");
                playerEntity.RoomUser.NeedsUpdate = true;

                playerEntity.Send(new YouAreControllerComposer(0));
            }
        }

        #endregion
    }
}
