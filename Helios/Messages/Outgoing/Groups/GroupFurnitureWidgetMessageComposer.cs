namespace Helios.Messages.Outgoing
{
    public class GroupFurnitureWidgetMessageComposer : IMessageComposer
    {
        private int itemId;
        private int groupId;
        private string groupName;
        private int roomId;

        public GroupFurnitureWidgetMessageComposer(int id, int v1, string v2, int v3)
        {
            this.itemId = id;
            this.groupId = v1;
            this.groupName = v2;
            this.roomId = v3;
        }

        public override void Write()
        {
            _data.Add(this.itemId);
            _data.Add(this.groupId);
            _data.Add(this.groupName);
            _data.Add(this.roomId);
        }
    }
}