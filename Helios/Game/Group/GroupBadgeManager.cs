using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using Helios.Storage.Models.Item;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Helios.Game
{
    public class GroupBadgeManager
    {

        #region Properties

        public List<GroupBadgeElementData> GroupBadgeElements { get; private set; }

        public Dictionary<int, GroupBadgeElementData> Base
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "base").ToDictionary(x => x.Id, x => x);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Symbol
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "symbol").ToDictionary(x => x.Id, x => x);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Colour1
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "colour1").ToDictionary(x => x.Id, x => x);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Colour2
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "colour2").ToDictionary(x => x.Id, x => x);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Colour3
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "colour3").ToDictionary(x => x.Id, x => x);
            }
        }


        #endregion

        #region Constructors

        public GroupBadgeManager()
        {
            GroupBadgeElements = GroupDao.GetGroupBadgeElementData();

            var t1 = Base;
            var t2 = Symbol;
            var t3 = Colour1;
            var t4 = Colour2;
            var t5 = Colour3;
        }

        #endregion

        #region Public methods

        

        #endregion
    }
}
