namespace Helios.Messages.Outgoing
{
    public class GroupFurnitureWidgetMessageComposer : IMessageComposer
    {
        private int itemId;
        private int groupId;
        private string groupName;
        private int roomId;
        private bool isMember;

        public GroupFurnitureWidgetMessageComposer(int id, int v1, string v2, int v3, bool isMember)
        {
            this.itemId = id;
            this.groupId = v1;
            this.groupName = v2;
            this.roomId = v3;
            this.isMember = isMember;
        }

        public override void Write()
        {
            _data.Add(this.itemId);
            _data.Add(this.groupId);
            _data.Add(this.groupName);
            _data.Add(this.roomId);
            _data.Add(this.isMember);
        }

        public override int HeaderId => -1;
    }
}