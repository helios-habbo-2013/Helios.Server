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

            if (avatar.RoomUser.Room == null)
                return;

            Room room = avatar.RoomUser.Room;

            if (room == null)
                return;

            Item item = room.ItemManager.GetItem(itemId);

            if (item == null)
                return;


            Group group = null;
            
            if (item.Data.GroupId is int groupId)
                group = GroupManager.Instance.GetGroup(groupId);

            avatar.Send(new GroupFurnitureWidgetMessageComposer(item.Id, item.Data.GroupId ?? 0, group?.Data.Name ?? "", group?.Data.RoomId ?? 0));
            /*
                var group = GroupManager.Instance.GetGroup(groupId);

                if (group == null)
                {
                    return;
                }
            */
        }
    }
}
