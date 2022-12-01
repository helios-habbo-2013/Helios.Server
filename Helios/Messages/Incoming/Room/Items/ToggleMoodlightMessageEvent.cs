﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class ToggleMoodlightMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            Room room = player.RoomUser.Room;

            if (room == null || !room.IsOwner(player.Details.Id))
                return;

            Item moodlight = room.ItemManager.Items.Values.SingleOrDefault(x => x.Definition.InteractorType == InteractorType.ROOMDIMMER);

            if (moodlight == null)
                return;

            var moodlightData = (MoodlightExtraData)moodlight.Interactor.GetJsonObject();
            moodlightData.Enabled = !moodlightData.Enabled;
            moodlight.Interactor.SetJsonObject(moodlightData);

            moodlight.Update();
            moodlight.Save();
        }
    }
}
