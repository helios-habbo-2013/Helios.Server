using System.Collections.Generic;
using System.Linq;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Misc;
using Helios.Storage.Models.Room;

namespace Helios.Messages.Incoming
{
    public class SaveRoomMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = avatar.RoomUser.Room;

            if (room == null || !room.RightsManager.IsOwner(avatar.Details.Id))
                return;

            int roomId = request.ReadInt();
            string name = request.ReadString();
            string description = request.ReadString();
            int roomAccess = request.ReadInt();
            string password = request.ReadString();
            int maxUsers = request.ReadInt();
            int categoryId = request.ReadInt();
            int tagCount = request.ReadInt();

            List<string> tags = new List<string>();

            for (int i = 0; i < tagCount; i++)
                tags.Add(request.ReadString().ToLower());

            int tradeSettings = request.ReadInt();
            bool allowPets = request.ReadBool();
            bool allowPetsEat = request.ReadBool();
            bool roomBlockingEnabled = request.ReadBool();
            bool hidewall = request.ReadBool();
            int wallThickness = request.ReadInt();
            int floorThickness = request.ReadInt();
            int whoMute = request.ReadInt();
            int whoKick = request.ReadInt();
            int whoBan = request.ReadInt();

            if (tradeSettings < 0 || tradeSettings > 2)
                tradeSettings = 0;

            if (whoMute < 0 || whoMute > 1)
                whoMute = 0;

            if (whoKick < 0 || whoKick > 2)
                whoKick = 0;

            if (whoBan < 0 || whoBan > 1)
                whoBan = 0;

            if (wallThickness < -2 || wallThickness > 1)
                wallThickness = 0;

            if (floorThickness < -2 || floorThickness > 1)
                floorThickness = 0;

            if (name.Length < 1)
                return;

            if (name.Length > 60)
                name = name.Substring(0, 60);

            if (maxUsers < 0)
                maxUsers = 10;

            if (maxUsers > 50)
                maxUsers = 50;

            if (tagCount > 2)
                return;

            room.Data.Name = name;
            room.Data.Description = description;
            room.Data.Status = (RoomStatus)roomAccess;
            room.Data.Password = password;
            room.Data.UsersMax = maxUsers;
            room.Data.CategoryId = categoryId;
            room.Data.TradeSetting = tradeSettings;
            room.Data.AllowPets = allowPets;
            room.Data.AllowPetsEat = allowPetsEat;
            room.Data.AllowWalkthrough = roomBlockingEnabled;
            room.Data.IsHidingWall = hidewall;
            room.Data.WallThickness = wallThickness;
            room.Data.FloorThickness = floorThickness;
            room.Data.WhoCanBan = (RoomBanSetting)whoBan;
            room.Data.WhoCanKick  = (RoomKickSetting)whoKick;
            room.Data.WhoCanMute = (RoomMuteSetting)whoMute;

            using (var context = new StorageContext())
            {
                context.DeleteRoomTags(room.Data.Id);

                foreach (var tag in tags.Distinct())
                {
                    context.SaveTag(new TagData
                    {
                        AvatarId = room.Data.Id,
                        RoomId = room.Data.Id,
                        Text = tag
                    });

                }

                context.SaveRoom(room.Data);
            }

            room.Send(new RoomSettingsSavedComposer(room.Data.Id));
            room.Send(new RoomInfoComposer(room.Data, true, false));
            room.Send(new RoomVisualizationSettingsComposer(room.Data.FloorThickness, room.Data.WallThickness, room.Data.IsHidingWall));
        }

        public int HeaderId => -1;
    }
}
