using System.Linq;
using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class SaveMoodlightMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null || !room.RightsManager.IsOwner(avatar.Details.Id))
                return;

            Item moodlight = room.ItemManager.Items.Values.SingleOrDefault(x => x.Definition.InteractorType == InteractorType.ROOMDIMMER);

            if (moodlight == null)
                return;

            int preset = request.ReadInt();
            bool isBackground = request.ReadInt() >= 2;
            string colourCode = request.ReadString();
            int intensity = request.ReadInt();

            if (preset <= 0 || preset > 3)
                preset = 1;

            var moodlightData = (MoodlightExtraData)moodlight.Interactor.GetJsonObject();

            moodlightData.CurrentPreset = preset;
            moodlightData.Enabled = true;
            moodlightData.Presets[moodlightData.CurrentPreset - 1].ColorCode = colourCode;
            moodlightData.Presets[moodlightData.CurrentPreset - 1].ColorIntensity = intensity;
            moodlightData.Presets[moodlightData.CurrentPreset - 1].IsBackground = isBackground;
            moodlight.Interactor.SetJsonObject(moodlightData);

            moodlight.Update();
            moodlight.Save();
        }
    }
}
