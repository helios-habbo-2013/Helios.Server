using Helios.Game;
using Helios.Messages.Incoming;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using System.Collections.Generic;

namespace Helios.Messages.Incoming
{
    class GetRoomAdMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;
            RoomModel roomModel = room.Model;


            avatar.Send(new RoomEntryInfoMessageComposer(room.Data, room.RightsManager.IsOwner(avatar.Details.Id)));

            room.Send(new UsersMessageComposer(List.Create<IEntity>(avatar)));
            room.Send(new UserUpdateMessageComposer(List.Create<IEntity>(avatar)));

            avatar.Send(new UsersMessageComposer(room.EntityManager.GetEntities<IEntity>()));
            avatar.Send(new UserUpdateMessageComposer(room.EntityManager.GetEntities<IEntity>()));

            avatar.Send(new ObjectsMessageComposer(room.ItemManager.Items));
            avatar.Send(new ItemsMessageComposer(room.ItemManager.Items));

            foreach (var entity in room.Entities.Values)
            {
                if (entity.RoomEntity.IsDancing)
                    avatar.Send(new DanceMessageComposer(entity.RoomEntity.InstanceId, entity.RoomEntity.DanceId));

                if (entity.RoomEntity.HasEffect)
                    avatar.Send(new AvatarEffectMessageComposer(entity.RoomEntity.InstanceId, entity.RoomEntity.EffectId));
            }

            if (avatar.RoomEntity.EffectId > 0)
                avatar.RoomEntity.UseEffect(avatar.RoomEntity.EffectId);
        }

        public int HeaderId => 126;
    }
}