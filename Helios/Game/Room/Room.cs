using Helios.Game.Managers;
using Helios.Messages;
using Helios.Messages.Outgoing;
using Helios.Storage.Models.Room;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Game
{
    public class Room
    {
        #region Properties

        public RoomData Data { get; }
        public RoomEntityManager EntityManager { get; }
        public RoomTaskManager TaskManager { get; }
        public RoomItemManager ItemManager { get; }
        public RoomMapping Mapping { get; set; }
        public RoomFurniture FurnitureManager { get; set; }
        public RoomModel Model => RoomManager.Instance.RoomModels.FirstOrDefault(x => x.Data.Id == Data.ModelId);
        public ConcurrentDictionary<int, IEntity> Entities { get; }
        public bool IsActive { get; set; }

        #endregion

        #region Constructors

        public Room(RoomData data)
        {
            Data = data;
            Entities = new ConcurrentDictionary<int, IEntity>();
            EntityManager = new RoomEntityManager(this);
            Mapping = new RoomMapping(this);
            TaskManager = new RoomTaskManager(this);
            ItemManager = new RoomItemManager(this);
            FurnitureManager = new RoomFurniture(this);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Wrap the retrieved database data with a room instance >:)
        /// </summary>
        public static Room Wrap(RoomData roomData)
        {
            return new Room(roomData);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get if the user has rights
        /// </summary>
        public bool HasRights(int AvatarId, bool checkOwner = true)
        {
            if (checkOwner)
                if (Data.OwnerId == AvatarId)
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
            if (Data.OwnerId == AvatarId)
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
        /// Try and dispose, only if it has 0 avatars active.
        /// </summary>
        public void TryDispose()
        {
            var avatarList = EntityManager.GetEntities<Avatar>();

            if (avatarList.Any())
                return;

            TaskManager.StopTasks();
            RoomManager.Instance.RemoveRoom(Data.Id);

            IsActive = false;
        }

        /// <summary>
        /// Send packet to entire avatar list in room
        /// </summary>
        public void Send(IMessageComposer composer, List<Avatar> specificUsers = null)
        {
            if (specificUsers == null)
                specificUsers = EntityManager.GetEntities<Avatar>();

            foreach (var avatar in specificUsers)
                avatar.Send(composer);
        }

        /// <summary>
        /// Forward entity to room
        /// </summary>
        /// <param name="entity"></param>
        public void Forward(IEntity entity)
        {
            if (!(entity is Avatar))
            {
                return;
            }

            var avatar = (Avatar)entity;
            
            avatar.Send(new RoomForwardComposer(Data.Id, Data.IsPublicRoom));
        }

        #endregion
    }
}
