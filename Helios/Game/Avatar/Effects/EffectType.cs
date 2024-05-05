using Helios.Storage.Models.Effect;

namespace Helios.Game
{
    public class EffectType
    {
        #region Properties

        public int EffectId { get; set; }
        public int Duration { get; set; }
        public bool IsCostume { get; set; }

        #endregion

        #region Constructor

        public EffectType(EffectSettingData settingData)
        {
            this.Duration = settingData.Duration;
            this.EffectId = settingData.EffectId;
            this.IsCostume = settingData.IsCostume;
        }

        #endregion
    }
}
