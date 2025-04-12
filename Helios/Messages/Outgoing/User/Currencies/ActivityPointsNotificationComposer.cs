using Helios.Network.Streams;
using Helios.Storage.Models.Catalogue;

namespace Helios.Messages.Outgoing
{
    class ActivityPointsNotificationComposer : IMessageComposer
    {
        public enum ActivityPointAlertType
        {
            PIXELS_RECEIVED,
            PIXELS_SOUND,
            NO_SOUND
        }

        private readonly int pixels;
        private readonly ActivityPointAlertType alertType;

        public ActivityPointsNotificationComposer(int pixels, ActivityPointAlertType alertType)
        {
            this.pixels = pixels;
            this.alertType = alertType;
        }

        public override void Write()
        {
            _data.Add(this.pixels);

            if (this.alertType == ActivityPointAlertType.PIXELS_RECEIVED)
            {
                _data.Add(15);
            }
            else if (this.alertType == ActivityPointAlertType.PIXELS_SOUND)
            {
                _data.Add(-1);
            }
            else if (this.alertType == ActivityPointAlertType.NO_SOUND)
            {
                _data.Add(0);
            }
        }

        public override int HeaderId => 438;
    }
}
