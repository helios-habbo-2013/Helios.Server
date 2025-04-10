using Helios.Storage;
using Helios.Storage.Access;
using Helios.Storage.Models.Catalogue;
using Helios.Storage.Models.Group;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Game
{
    public class Group
    {
        #region Properties

        public GroupData Data { get; }
        public List<GroupMembership> Members { get; private set; }
        public string ColourA => GroupManager.Instance.BadgeManager.Colour2[Data.Colour1].FirstValue;
        public string ColourB => GroupManager.Instance.BadgeManager.Colour3[Data.Colour2].FirstValue;

        #endregion

        #region Constructors

        public Group(GroupData data)
        {
            Data = data;
            Members = data.GroupMemberships.Select(x => new GroupMembership(x)).ToList();
        }

        #endregion

        #region Public methods

        public bool IsMemberType(int avatarId, GroupMembershipType memberType)
        {
            return this.Members.Any(x => x.Data.AvatarId == avatarId && x.Data.MemberType == memberType);
        }

        public GroupMembershipType GetMemberType(int avatarId)
        {
            if (this.Members.Any(x => x.Data.AvatarId == avatarId))
            {
                return this.Members.FirstOrDefault(x => x.Data.AvatarId == avatarId).Data.MemberType;
            } 
            else
            {
                return GroupMembershipType.NONE;
            }
        }

        public bool IsAdmin(int avatarId)
        {
             return this.Members.Any(x =>
                    x.Data.MemberType == GroupMembershipType.ADMIN &&
                    x.Data.AvatarId == avatarId) || this.Data.OwnerId == avatarId;
        }

        public void RefreshMembers()
        {
            using (var context = new StorageContext())
            {
                Members = context.GetGroupMembers(this.Data.Id).Select(x => new GroupMembership(x)).ToList();
            }
        }

        #endregion
    }
}
