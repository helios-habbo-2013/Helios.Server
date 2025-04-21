using Helios.Game;
using System.Collections.Generic;

namespace Helios.Messages.Outgoing
{
    class UnseenItemsComposer : IMessageComposer
    {
        private Dictionary<FurniListNotificationType, List<int>> _notifications;

        public UnseenItemsComposer(List<Item> items)
        {
            _notifications = new Dictionary<FurniListNotificationType, List<int>>();

            foreach (var item in items)
            {
                // Code this shit better at some point
                if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
                {
                    if (!_notifications.ContainsKey(FurniListNotificationType.GENERIC_WALL))
                        _notifications.Add(FurniListNotificationType.GENERIC_WALL, new List<int>());
                }
                else
                {
                    if (!_notifications.ContainsKey(FurniListNotificationType.GENERIC_FLOOR))
                        _notifications.Add(FurniListNotificationType.GENERIC_FLOOR, new List<int>());
                }


                if (item.Definition.HasBehaviour(ItemBehaviour.WALL_ITEM))
                {
                    _notifications[FurniListNotificationType.GENERIC_WALL].Add(item.Id);
                }
                else
                {
                    _notifications[FurniListNotificationType.GENERIC_FLOOR].Add(item.Id);
                }
            }
        }

        public override void Write()
        {
            /*m_Data.Add(notifications.Count);

            foreach (var key in notifications.Values)
                m_Data.Add((int)key);

            m_Data.Add(notifications.Count);

            foreach (var value in notifications.Keys)
                m_Data.Add(value);*/

            this.AppendInt32(_notifications.Count);

            foreach (var kvp in _notifications)
            {
                this.AppendInt32((int)kvp.Key);
                this.AppendInt32(kvp.Value.Count);

                foreach (int itemId in kvp.Value)
                {
                    this.AppendInt32(itemId);
                }
            }

        }

        public override int HeaderId => 832;
    }

    public enum FurniListNotificationType
    {
        GENERIC_FLOOR = 1,
        GENERIC_WALL = 2,
        PET = 3,
        BADGE = 4,
        BOT = 5,

    }
}