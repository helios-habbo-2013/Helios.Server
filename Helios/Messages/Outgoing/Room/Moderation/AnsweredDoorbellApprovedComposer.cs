using Helios.Game;
using Helios.Network.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Messages.Outgoing
{
    internal class AnsweredDoorbellApprovedComposer : IMessageComposer
    {
        public override void Write()
        {

        }

        public override int HeaderId => 41;


    }
}
