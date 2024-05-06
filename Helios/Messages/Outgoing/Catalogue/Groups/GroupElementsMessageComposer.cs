using System;
using System.Collections.Generic;
using Helios.Game;
using Helios.Storage.Models.Group;

namespace Helios.Messages.Outgoing
{
    class GroupElementsMessageComposer : IMessageComposer
    {
        private Dictionary<int, GroupBadgeElementData> badgeBase;
        private Dictionary<int, GroupBadgeElementData> badgeSymbol;
        private Dictionary<int, GroupBadgeElementData> badgeColour1;
        private Dictionary<int, GroupBadgeElementData> badgeColour2;
        private Dictionary<int, GroupBadgeElementData> badgeColour3;


        public GroupElementsMessageComposer(Dictionary<int, GroupBadgeElementData> badgeBase, Dictionary<int, GroupBadgeElementData> badgeSymbol, Dictionary<int, GroupBadgeElementData> badgeColour1, Dictionary<int, GroupBadgeElementData> badgeColour2, Dictionary<int, GroupBadgeElementData> badgeColour3)
        {
            this.badgeBase = badgeBase;
            this.badgeSymbol = badgeSymbol;
            this.badgeColour1 = badgeColour1;
            this.badgeColour2 = badgeColour2;
            this.badgeColour3 = badgeColour3;
        }

        public override void Write()
        {
            _data.Add(this.badgeBase.Count);

            foreach (var element in this.badgeBase.Values)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
                _data.Add(element.SecondValue);
            }

            _data.Add(this.badgeSymbol.Count);

            foreach (var element in this.badgeSymbol.Values)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
                _data.Add(element.SecondValue);
            }

            _data.Add(this.badgeColour1.Count);

            foreach (var element in this.badgeColour1.Values)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
            }

            _data.Add(this.badgeColour2.Count);

            foreach (var element in this.badgeColour2.Values)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
            }

            _data.Add(this.badgeColour3.Count);

            foreach (var element in this.badgeColour3.Values)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
            }
        }
    }
}
