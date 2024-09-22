using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class ManageGroupMessageComposer : IMessageComposer
    {
        private Group group;

        public ManageGroupMessageComposer(Group group)
        {
            this.group = group;
        }

        public override void Write()
        {
            _data.Add(0);
            _data.Add(true);
            _data.Add(group.Data.Id);

            _data.Add(group.Data.Name);
            _data.Add(group.Data.Description);

            _data.Add(1);
            _data.Add(group.Data.Colour1);
            _data.Add(group.Data.Colour2);
            _data.Add((int) group.Data.GroupType);
            _data.Add(group.Data.AllowMembersDecorate ? 0 : 1);

            _data.Add(false);
            _data.Add("");

            var partMatches = System.Text.RegularExpressions.Regex.Matches(group.Data.Badge, @"[b|s][0-9]{4,6}");
            _data.Add((int) partMatches.Count);

            foreach (System.Text.RegularExpressions.Match partMatch in partMatches)
            {
                string partCode = partMatch.Value;
                char partType = partCode[0];

                int partId = 0; // int.Parse(partCode.Substring(1, 2));
                int partColor = 0;// int.Parse(partCode.Substring(3, 2));
                int partPosition = 0;// int.Parse(partCode.Substring(5));

                if (partType == 'b')
                {
                    partId = int.Parse(partCode.Substring(1, 3));
                    partColor = int.Parse(partCode.Substring(4));
                    partPosition = 0;
                }
                else
                {
                    partId = int.Parse(partCode.Substring(1, 3));
                    partColor = int.Parse(partCode.Substring(4, partCode.Substring(4).Length - 1));
                    partPosition = int.Parse(partCode.Substring(partCode.Length - 1, 1));
                }

                _data.Add(partId);
                _data.Add(partColor);
                _data.Add(partPosition);
            }

            _data.Add(group.Data.Badge);
            _data.Add(group.Members.Count - group.Members.Count(x => x.Data.MemberType == Storage.Models.Group.GroupMembershipType.PENDING));
        }
    }
}
