using Helios.Storage.Models.Misc;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class FavouriteRoomsComposer : IMessageComposer
    {

        public override void Write()
        {
            this.AppendInt32(30);
            this.AppendInt32(0);
        }

        public override int HeaderId => 458;
    }
}