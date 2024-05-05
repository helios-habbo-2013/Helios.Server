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

        public Dictionary<int, GroupBadgeElementData> GroupBadgeElements { get; private set; }

        public Dictionary<int, GroupBadgeElementData> Base
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Value.Type == "base").ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Symbol
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Value.Type == "symbol").ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Colour1
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Value.Type == "colour1").ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Colour2
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Value.Type == "colour2").ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public Dictionary<int, GroupBadgeElementData> Colour3
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Value.Type == "colour3").ToDictionary(x => x.Key, x => x.Value);
            }
        }


        #endregion

        #region Constructors

        public GroupBadgeManager()
        {
            GroupBadgeElements = GroupDao.GetGroupBadgeElementData().ToDictionary(x => x.Id, y => y);
        }

        #endregion

        #region Public methods

        

        #endregion
    }
}
