using Helios.Messages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Helios.Game
{
    public class MoodlightInteractor : Interactor
    {
        #region Overridden Properties



        #endregion

        public MoodlightInteractor(Item item) : base(item)
        {

            MoodlightExtraData extraData = null;

            try
            {
                extraData = JsonConvert.DeserializeObject<MoodlightExtraData>(Item.Data.ExtraData);
            }
            catch { }

            if (extraData == null)
            {
                extraData = new MoodlightExtraData
                {
                    CurrentPreset = 1,
                    Presets = new List<MoodlightPresetData>
                            {
                                new MoodlightPresetData(),
                                new MoodlightPresetData(),
                                new MoodlightPresetData()
                            }
                };
            }

            SetExtraData(extraData);
        }

        public override void WriteExtraData(IMessageComposer composer, bool inventoryView = false)
        {
            var data = GetJsonObject<MoodlightExtraData>();
            var preset = data.Presets[data.CurrentPreset - 1];

            StringBuilder builder = new StringBuilder();
            builder.Append(data.Enabled ? 2 : 1);
            builder.Append(",");
            builder.Append(data.CurrentPreset);
            builder.Append(",");
            builder.Append(preset.IsBackground ? 2 : 1);
            builder.Append(",");
            builder.Append(preset.ColorCode);
            builder.Append(",");
            builder.Append(preset.ColorIntensity);

            composer.Data.Add((int)ExtraDataType.Legacy);
            composer.Data.Add(builder.ToString());
        }
    }
}
