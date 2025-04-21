using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class NavigatorSettingsComposer : IMessageComposer
    {
        private int v;

        public NavigatorSettingsComposer(int v)
        {
            this.v = v;
        }

        public override void Write()
        {

        }

        public override int HeaderId => 455;
    }
}