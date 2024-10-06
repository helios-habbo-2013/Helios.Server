using System.Linq;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    class GetMoodlightConfigEvent : IMessageEvent
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

            MoodlightExtraData moodlightData = moodlight.Interactor.GetJsonObject<MoodlightExtraData>();

            avatar.Send(new MoodlightConfigComposer(moodlightData));
        }
    }
}
