using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class MOTDNotificationComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 810;
    }
}