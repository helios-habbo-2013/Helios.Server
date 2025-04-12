using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class MoodlightConfigComposer : IMessageComposer
    {
        private MoodlightExtraData moodlightData;

        public MoodlightConfigComposer(MoodlightExtraData moodlightData)
        {
            this.moodlightData = moodlightData;
        }

        public override void Write()
        {
            _data.Add(moodlightData.Presets.Count);
            _data.Add(moodlightData.CurrentPreset);

            int i = 1;
            foreach (var preset in moodlightData.Presets)
            {
                _data.Add(i);
                _data.Add(preset.IsBackground ? 2 : 1);
                _data.Add(preset.ColorCode);
                _data.Add(preset.ColorIntensity);
                i++;
            }
        }

        public int HeaderId => -1;
    }
}
