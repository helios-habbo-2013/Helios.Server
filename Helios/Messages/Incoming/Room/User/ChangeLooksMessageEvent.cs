using System;
using System.Collections.Generic;
using System.Text;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Database.Access;
using Helios.Util.Extensions;

namespace Helios.Messages.Incoming
{
    class ChangeLooksMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            // sh-300-64.ea-1402-62.cc-260-62.ca-1806-73.ha-1008-62.lg-270-64.hd-180-1
            Room room = player.RoomEntity.Room;

            if (room == null)
                return;

            string sex = request.ReadString().FilterInput().ToUpper();
            string figure = request.ReadString().FilterInput();

            if (sex != "M" && sex != "F")
                return;

            player.Details.Figure = figure;
            player.Details.Sex = sex;

            PlayerDao.Update(player.Details);

            player.Send(new UserChangeMessageComposer(-1, player.Details.Figure, player.Details.Sex, player.Details.Motto, player.Details.AchievementPoints));
            room.Send(new UserChangeMessageComposer(player.RoomEntity.InstanceId, player.Details.Figure, player.Details.Sex, player.Details.Motto, player.Details.AchievementPoints));
        }
    }
}
