using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Messages.Incoming
{
    public class GetRoomRightsMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var room = avatar.RoomUser.Room;

            if (room == null)
            {
                return;
            }

            using (var context = new StorageContext())
            {
                var rightsList = context.GetRoomRights(room.Data.Id);

                avatar.Send(new RightsListMessageComposer(room.Data.Id, rightsList));
            }
        }

        public int HeaderId => -1;
    }
}
