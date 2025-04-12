using System.Linq;
using Helios.Game;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class ToggleMoodlightMessageEvent : IMessageEvent
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

            var moodlightData = moodlight.Interactor.GetJsonObject<MoodlightExtraData>();
            moodlightData.Enabled = !moodlightData.Enabled;
            moodlight.Interactor.SetExtraData(moodlightData);

            moodlight.Update();
            moodlight.Save();
        }

        public int HeaderId => -1;
    }
}
