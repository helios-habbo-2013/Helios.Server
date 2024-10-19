using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Messages.Outgoing.Catalogue.Groups;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Room;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Helios.Messages.Incoming.Catalogue
{
    class PurchaseGroupMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Subscription == null)
            {
                return;
            }

            string name = request.ReadString();
            string desc = request.ReadString();

            int roomId = request.ReadInt();
            int colour1 = request.ReadInt();
            int colour2 = request.ReadInt();

            int badgeElements = request.ReadInt() / 3;

            StringBuilder badgeBuilder = new StringBuilder();

            for (int i = 0; i < badgeElements; i++)
            {
                if (i > 0)
                {
                    badgeBuilder.Append("s");
                }
                else
                {
                    badgeBuilder.Append("b");
                }

                int badgeElementId = request.ReadInt();
                int badgeElementColour = request.ReadInt();
                int badgeElementPosition = request.ReadInt();

                badgeBuilder.Append(badgeElementId.ToString().PadLeft(3, '0'));
                badgeBuilder.Append(badgeElementColour.ToString());

                if (i > 0)
                {
                    badgeBuilder.Append(badgeElementPosition);
                }
                //else
                //{
                //    badgeBuilder.Append("X");
                //}
            }

            using (var context = new GameStorageContext())
            {
                var roomList = RoomManager.Instance.ReplaceQueryRooms(
                    new List<RoomData>() { context.GetRoomData(roomId) }
                );

                var room = roomList.FirstOrDefault();

                if (room == null || room.Data.GroupId != null || !room.RightsManager.IsOwner(avatar.Details.Id))
                {
                    return;
                }

                var groupData = new GroupData
                {
                    OwnerId = avatar.EntityData.Id,
                    RoomId = roomId,
                    Name = name,
                    Description = desc,
                    Colour1 = colour1,//GroupManager.Instance.BadgeManager.Colour2[colour1].FirstValue,
                    Colour2 = colour2,//GroupManager.Instance.BadgeManager.Colour3[colour2].FirstValue,
                    Badge = badgeBuilder.ToString()
                };

                context.UpdateGroup(groupData);

                room.Data.GroupId = groupData.Id;
                context.SaveRoom(room.Data);
                
                avatar.Details.FavouriteGroupId = groupData.Id;
                context.ChangeTracker.Clear();
                context.Update(avatar.Details);
                context.SaveChanges();

                /*
                if (room != null)
                {
                    // if (avatar.RoomUser.Room == null || avatar.RoomUser.Room.Data.Id != roomId)
                    //    room.Forward(avatar);

                    var rightsList = context.GetRoomRights(room.Data.Id);

                    foreach (var toRemove in rightsList)
                    {
                        room.RightsManager.RemoveRights(toRemove.AvatarData.Id, false);

                        avatar.Send(new RemoveRightsMessageComposer(room.Data.Id, toRemove.AvatarData.Id));
                    }

                    room.Send(new GroupBadgesMessageComposer(groupData.Id, groupData.Badge));
                    room.Send(new UserRemoveComposer(avatar.RoomUser.InstanceId));
                    room.Send(new UsersComposer(List.Create(avatar as IEntity)));

                    avatar.RoomUser.NeedsUpdate = true;
                }
                */

                context.ClearRoomRights(room.Data.Id);

                avatar.Send(new GroupRoomMessageComposer(roomId, groupData.Id));
            }
        }
    }
}
