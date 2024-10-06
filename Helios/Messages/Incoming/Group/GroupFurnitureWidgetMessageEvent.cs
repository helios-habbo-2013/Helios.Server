using DotNetty.Common.Utilities;
using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using MySql.Data.MySqlClient.Memcached;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Messages.Incoming
    {
        public class GroupFurnitureWidgetMessageEvent : IMessageEvent
        {
            public void Handle(Avatar avatar, Request request)
            {
                int itemId = request.ReadInt();
                int groupId = request.ReadInt();

                var group = GroupManager.Instance.GetGroup(groupId);

                if (group == null)
                {
                    return;
                }

            }
        }
    }
