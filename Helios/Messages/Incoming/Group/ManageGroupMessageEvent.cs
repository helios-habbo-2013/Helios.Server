using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;

namespace Helios.Messages.Incoming
{
    public class ManageGroupMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            int groupId = request.ReadInt();

            var group = GroupManager.Instance.GetGroup(groupId);

            if (group == null || group.Data.OwnerId != avatar.EntityData.Id)
            {
                return;
            }

            avatar.Send(new GroupElementsMessageComposer(
                GroupManager.Instance.BadgeManager.Base,
                GroupManager.Instance.BadgeManager.Symbol,
                GroupManager.Instance.BadgeManager.Colour1,
                GroupManager.Instance.BadgeManager.Colour2,
                GroupManager.Instance.BadgeManager.Colour3
            ));

            avatar.Send(new ManageGroupMessageComposer(group));
        }

    }
}
