using Helios.Storage.Access;
using Helios.Storage.Models.Catalogue;
using Helios.Storage.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Game
{
    public class GroupMembership
    {
        #region Properties

        public GroupMembershipData Data { get; }

        #endregion

        #region Constructors

        public GroupMembership(GroupMembershipData data)
        {
            Data = data;
        }

        #endregion

        #region Public methods


        #endregion
    }
}
