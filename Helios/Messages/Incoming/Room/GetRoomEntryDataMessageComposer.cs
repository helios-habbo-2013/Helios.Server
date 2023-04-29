using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using System.Collections.Generic;

namespace Helios.Messages.Incoming
{
    class GetRoomEntryDataMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;
            RoomModel roomModel = room.Model;

            avatar.Send(new HeightMapComposer(roomModel.Heightmap));
            avatar.Send(new FloorHeightMapComposer(roomModel.Heightmap));
            avatar.Send(new RoomVisualizationSettingsComposer(room.Data.FloorThickness, room.Data.WallThickness, room.Data.IsHidingWall));
            avatar.Send(new RoomEntryInfoComposer(room.Data, room.IsOwner(avatar.Details.Id)));

            room.Send(new UsersComposer(List.Create<IEntity>(avatar)));
            room.Send(new UsersStatusComposer(List.Create<IEntity>(avatar)));

            avatar.Send(new UsersComposer(room.EntityManager.GetEntities<IEntity>()));
            avatar.Send(new UsersStatusComposer(room.EntityManager.GetEntities<IEntity>()));

            avatar.Send(new RoomInfoComposer(room.Data, true, false)); // false, true));

            avatar.Send(new FloorItemsComposer(room.ItemManager.Items));
            avatar.Send(new WallItemsComposer(room.ItemManager.Items));

            foreach (var entity in room.Entities.Values)
            {
                if (entity.RoomEntity.IsDancing)
                    avatar.Send(new DanceMessageComposer(entity.RoomEntity.InstanceId, entity.RoomEntity.DanceId));

                if (entity.RoomEntity.HasEffect)
                    avatar.Send(new EffectMessageComposer(entity.RoomEntity.InstanceId, entity.RoomEntity.EffectId));
            }

            if (avatar.RoomEntity.EffectId > 0)
                avatar.RoomEntity.UseEffect(avatar.RoomEntity.EffectId);
        }
    }
}
