using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Item;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Helios.Game
{
    public class GroupManager : ILoadable
    {
        #region Fields

        public static readonly GroupManager Instance = new GroupManager();

        #endregion

        #region Properties

        public GroupBadgeManager BadgeManager { get; private set; }
        public int Cost => 10;

        #endregion

        #region Constructors

        public void Load()
        {
            BadgeManager = new GroupBadgeManager();
        }

        #endregion

        #region Public methods

        public Group GetGroup(int groupId)
        {
            using (var context = new GameStorageContext())
            {
                var data = GroupDao.GetGroup(context, groupId);

                if (data != null)
                    return new Group(data);
            }

            return null;
        }

        public List<Group> GetGroupsByMembership(int avatarId, params GroupMembershipType[] membershipTypes)
        {
            var membershipTypeList = new List<GroupMembershipType>();

            if (!membershipTypes.Any())
            {
                membershipTypeList.Add(GroupMembershipType.MEMBER);
                membershipTypeList.Add(GroupMembershipType.ADMIN);
            }

            using (var context = new GameStorageContext())
            {
                return GroupDao.GetGroupsByMembership(context, avatarId)
                    .Select(group => new Group(group))
                    .Where(x => x.Data.OwnerId == avatarId || x.Members.Any(x => x.Data.AvatarId == avatarId && membershipTypes.Any(membershipType => membershipType == x.Data.MemberType)))
                    .ToList();
            }
        }

        public bool HasGroup(int groupId)
        {
            using (var context = new GameStorageContext())
            {
                return context.GroupData.Any(x => x.Id == groupId);
            }
        }

        #endregion
    }
}
