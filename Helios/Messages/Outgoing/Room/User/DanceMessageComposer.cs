namespace Helios.Messages.Outgoing
{
    public class DanceMessageComposer : IMessageComposer
    {
        private int instanceId;
        private int danceId;

        public DanceMessageComposer(int instanceId, int danceId)
        {
            this.instanceId = instanceId;
            this.danceId = danceId;
        }

        public override void Write()
        {
            _data.Add(instanceId);
            _data.Add(danceId);
        }

        public override int HeaderId => 480;
    }
}
