using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    class ChangeLooksMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            // sh-300-64.ea-1402-62.cc-260-62.ca-1806-73.ha-1008-62.lg-270-64.hd-180-1
            Room room = avatar.RoomEntity.Room;

            if (room == null)
                return;

            string sex = request.ReadString().FilterInput().ToUpper();
            string figure = request.ReadString().FilterInput();

            if (sex != "M" && sex != "F")
                return;

            avatar.Details.Figure = figure;
            avatar.Details.Sex = sex;

            using (var context = new StorageContext())
            {
                context.AvatarData.Update(avatar.Details);
                context.SaveChanges();
            }

            avatar.Send(new UserChangeMessageComposer(-1, avatar.Details.Figure, avatar.Details.Sex, avatar.Details.Motto, avatar.Details.AchievementPoints));
            room.Send(new UserChangeMessageComposer(avatar.RoomEntity.InstanceId, avatar.Details.Figure, avatar.Details.Sex, avatar.Details.Motto, avatar.Details.AchievementPoints));
        }
    }
}
