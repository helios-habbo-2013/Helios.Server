using Helios.Game;
using Helios.Messages.Outgoing;
using Helios.Network.Streams;
using Helios.Storage.Models.Group;

namespace Helios.Messages.Incoming.Catalogue
{
    class GroupFurnitureCatalogMessageEvent : IMessageEvent
    {
        public void Handle(Avatar avatar, Request request)
        {
            var groupList = GroupManager.Instance.GetGroupsByMembership(avatar.Details.Id, GroupMembershipType.ADMIN, GroupMembershipType.MEMBER);

            avatar.Send(new GroupFurniConfigMessageComposer(
                avatar.Details.Id,
                groupList));
        }
    }
}
