﻿using Helios.Messages.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Util.Extensions;
using System.Threading.Tasks;
using Helios.Storage.Access;

namespace Helios.Game
{
    public class RoomEntityManager
    {
        #region Fields

        private Room room;
        private int instanceCounter;

        #endregion

        #region Constructors

        public RoomEntityManager(Room room)
        {
            this.room = room;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generate instance ID for new entity that entered room
        /// </summary>
        private int GenerateInstanceId()
        {
            return instanceCounter++;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Select entities where it's assignable by entity type
        /// </summary>
        public List<T> GetEntities<T>()
        {
            return room.Entities
                .Where(x => x.Value.GetType().IsAssignableFrom(typeof(T)) || x.Value.GetType().GetInterfaces().Contains(typeof(T)))
                .Select(x => x.Value).Cast<T>().ToList();
        }

        /// <summary>
        /// Enter room handler, used when user clicks room to enter
        /// </summary>
        public void EnterRoom(IEntity entity, Position entryPosition = null)
        {
            SilentlyEntityRoom(entity, entryPosition);

            if (!(entity is Avatar avatar))
                return;

            avatar.Send(new RoomReadyComposer(room.Model.Data.Model, room.Data.Id));

            if (room.Data.Wallpaper != "0")
                avatar.Send(new RoomPropertyComposer("wallpaper", Convert.ToString(room.Data.Wallpaper)));

            if (room.Data.Floor != "0")
                avatar.Send(new RoomPropertyComposer("floor", Convert.ToString(room.Data.Floor)));

            avatar.Send(new RoomPropertyComposer("landscape", Convert.ToString(room.Data.Landscape)));
            
            if (!room.Data.IsPublicRoom)
            {
                if (room.IsOwner(avatar.Details.Id))
                {
                    avatar.Send(new YouAreOwnerMessageEvent());
                    avatar.Send(new YouAreControllerComposer(4));
                }
                else if (room.HasRights(avatar.Details.Id, false))
                {
                    avatar.Send(new YouAreControllerComposer(1));
                }
            }
        }

        /// <summary>
        /// Silently enter room handler for every other entity type
        /// </summary>
        public void SilentlyEntityRoom(IEntity entity, Position entryPosition = null)
        {
            if (entity.RoomEntity.Room != null)
                entity.RoomEntity.Room.EntityManager.LeaveRoom(entity);

            if (!RoomManager.Instance.HasRoom(room.Data.Id))
                RoomManager.Instance.AddRoom(room);

            entity.RoomEntity.Reset();
            entity.RoomEntity.Room = room;
            entity.RoomEntity.InstanceId = GenerateInstanceId();
            entity.RoomEntity.Position = (entryPosition ?? room.Model.Door);
            entity.RoomEntity.AuthenticateRoomId = null;

            room.Entities.TryAdd(entity.RoomEntity.InstanceId, entity);

            if (!room.IsActive)
                TryInitialise();

            if (!string.IsNullOrEmpty(entity.RoomEntity.AuthenticateTeleporterId))
            {
                var teleporter = room.ItemManager.Items.Values.Where(x => x.Data.Id.ToString() == entity.RoomEntity.AuthenticateTeleporterId).FirstOrDefault();

                if (teleporter != null)
                {
                    entity.RoomEntity.WalkingAllowed = false;
                    entity.RoomEntity.Position = teleporter.Position.Copy();

                    teleporter.UpdateState(TeleporterInteractor.TELEPORTER_EFFECTS);

                    Task.Delay(1000).ContinueWith(t =>
                    {
                        teleporter.UpdateState(TeleporterInteractor.TELEPORTER_OPEN);
                        entity.RoomEntity.WalkingAllowed = true;

                        entity.RoomEntity.Move(
                            teleporter.Position.GetSquareInFront().X,
                            teleporter.Position.GetSquareInFront().Y
                        );
                    });

                    Task.Delay(2000).ContinueWith(t =>
                    {
                        teleporter.UpdateState(TeleporterInteractor.TELEPORTER_CLOSE);
                    });
                }

                entity.RoomEntity.AuthenticateTeleporterId = null;
            }

            if (entity is Avatar avatar)
            {
                // We're in room, yayyy >:)
                avatar.Messenger.SendStatus();

                room.Data.UsersNow++;
                RoomDao.SetVisitorCount(room.Data.Id, room.Data.UsersNow);
            }
            else
            {
                // For 'Avatar' to show, see GetRoomEntryDataMessageComposer.cs line 21
                room.Send(new UsersComposer(List.Create(entity)));
            }
        }

        /// <summary>
        /// Try initialize room to start
        /// </summary>
        private void TryInitialise()
        {
            if (room.IsActive)
                return;

            room.ItemManager.Load();
            room.TaskManager.Load();
            room.Mapping.Load();
            room.IsActive = true;
        }

        /// <summary>
        /// Leave room handler, called when user leaves room, clicks another room, re-enters room, and disconnects
        /// </summary>
        public void LeaveRoom(IEntity entity, bool hotelView = false)
        {
            room.Entities.Remove(entity.RoomEntity.InstanceId);
            room.Send(new UserRemoveComposer(entity.RoomEntity.InstanceId));

            // Remove entity from their current position
            var currentTile = entity.RoomEntity.Position.GetTile(room);

            if (currentTile != null)
                currentTile.RemoveEntity(entity);

            // Remove entity from their next position (if they were walking)
            var nextPosition = entity.RoomEntity.Next;

            if (nextPosition != null)
            {
                var nextTile = nextPosition.GetTile(room);

                if (nextTile != null)
                    nextTile.RemoveEntity(entity);
            }

            entity.RoomEntity.Reset();

            if (entity is Avatar avatar)
            {
                room.Data.UsersNow--;
                RoomDao.SetVisitorCount(room.Data.Id, room.Data.UsersNow);

                if (hotelView)
                    avatar.Send(new CloseConnectionComposer());
                
                avatar.Messenger.SendStatus();
            }

            room.TryDispose();
        }

        #endregion
    }
}
