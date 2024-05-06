using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Messages.Outgoing.Catalogue.Groups;
using Helios.Network.Streams;
using Helios.Storage.Access;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Helios.Messages.Incoming.Catalogue
{
    class PurchaseGroupMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            if (avatar.Subscription == null)
            {
                return;
            }

            string name = request.ReadString();
            string desc = request.ReadString();

            int roomId = request.ReadInt();

            var roomList = RoomDao.GetUserRooms(avatar.Details.Id)
                .Where(x => x.GroupId == null)
                .ToList();

            if (!roomList.Any(x => x.Id == roomId)) {
                return;
            }

            int colour1 = request.ReadInt();
            int colour2 = request.ReadInt();

            int badgeElements = request.ReadInt() / 3;

            StringBuilder badgeBuilder = new StringBuilder();

            for (int i = 0; i < badgeElements; i++)
            {
                if (i > 0)
                {
                    badgeBuilder.Append("s");
                }
                else
                {
                    badgeBuilder.Append("b");
                }

                int badgeElementId = request.ReadInt();
                int badgeElementColour = request.ReadInt();
                int badgeElementPosition = request.ReadInt();

                badgeBuilder.Append(badgeElementId.ToString().PadLeft(3, '0'));
                badgeBuilder.Append(badgeElementColour.ToString());

                if (i > 0)
                {
                    badgeBuilder.Append(badgeElementPosition);
                }
                //else
                //{
                //    badgeBuilder.Append("X");
                //}
            }

            var badgeCode = badgeBuilder.ToString();


        }
    }
}
