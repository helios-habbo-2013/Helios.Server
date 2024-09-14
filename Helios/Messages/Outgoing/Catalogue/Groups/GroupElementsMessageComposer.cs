using System;
using System.Collections.Generic;
using Helios.Game;
using Helios.Storage.Models.Group;

namespace Helios.Messages.Outgoing
{
    class GroupElementsMessageComposer : IMessageComposer
    {
        private List<GroupBadgeElementData> badgeBase;
        private List<GroupBadgeElementData> badgeSymbol;
        private List<GroupBadgeElementData> badgeColour1;
        private List<GroupBadgeElementData> badgeColour2;
        private List<GroupBadgeElementData> badgeColour3;


        public GroupElementsMessageComposer(List<GroupBadgeElementData> badgeBase, List<GroupBadgeElementData> badgeSymbol, List<GroupBadgeElementData> badgeColour1, List<GroupBadgeElementData> badgeColour2, List<GroupBadgeElementData> badgeColour3)
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

            foreach (var element in this.badgeBase)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
                _data.Add(element.SecondValue);
            }

            _data.Add(this.badgeSymbol.Count);

            foreach (var element in this.badgeSymbol)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
                _data.Add(element.SecondValue);
            }

            _data.Add(this.badgeColour1.Count);

            foreach (var element in this.badgeColour1)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
            }

            _data.Add(this.badgeColour2.Count);

            foreach (var element in this.badgeColour2)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
            }

            _data.Add(this.badgeColour3.Count);

            foreach (var element in this.badgeColour3)
            {
                _data.Add(element.Id);
                _data.Add(element.FirstValue);
            }
        }
    }
}
