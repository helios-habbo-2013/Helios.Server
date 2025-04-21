using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class HabboActivityPointNotificationMessageComposer : IMessageComposer
    {
        public enum ActivityPointAlertType
        {
            PIXELS_RECEIVED,
            PIXELS_SOUND,
            NO_SOUND
        }

        private readonly int _pixels;
        private readonly ActivityPointAlertType _alertType;

        public HabboActivityPointNotificationMessageComposer(int pixels, ActivityPointAlertType alertType)
        {
            this._pixels = pixels;
            this._alertType = alertType;
        }

        public override void Write()
        {
            _data.Add(this._pixels);

            if (this._alertType == ActivityPointAlertType.PIXELS_RECEIVED)
            {
                _data.Add(15);
            }
            else if (this._alertType == ActivityPointAlertType.PIXELS_SOUND)
            {
                _data.Add(-1);
            }
            else if (this._alertType == ActivityPointAlertType.NO_SOUND)
            {
                _data.Add(0);
            }
        }


        public override int HeaderId => 438;
    }
}