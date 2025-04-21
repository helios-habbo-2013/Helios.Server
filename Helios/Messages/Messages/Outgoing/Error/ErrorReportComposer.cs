using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ErrorReportComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 299;
    }
}