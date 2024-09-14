using Helios.Storage;
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

        public List<GroupBadgeElementData> Base
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "base").ToList();
            }
        }

        public List<GroupBadgeElementData> Symbol
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "symbol").ToList();
            }
        }

        public List<GroupBadgeElementData> Colour1
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "colour1").ToList();
            }
        }

        public List<GroupBadgeElementData> Colour2
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "colour2").ToList();
            }
        }

        public List<GroupBadgeElementData> Colour3
        {
            get
            {
                return GroupBadgeElements.Where(x => x.Type == "colour3").ToList(); // (x => x.Id, x => x);
            }
        }


        #endregion

        #region Constructors

        public GroupBadgeManager()
        {
            using (var context = new GameStorageContext())
            {
                GroupBadgeElements = context.GetGroupBadgeElementData();

                var t1 = Base;
                var t2 = Symbol;
                var t3 = Colour1;
                var t4 = Colour2;
                var t5 = Colour3;
            }
        }

        #endregion

        #region Public methods

        

        #endregion
    }
}
