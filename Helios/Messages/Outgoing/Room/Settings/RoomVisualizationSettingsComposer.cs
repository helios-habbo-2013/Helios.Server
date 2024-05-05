namespace Helios.Messages.Outgoing
{
    class RoomVisualizationSettingsComposer : IMessageComposer
    {
        private int floorThickness;
        private int wallThickness;
        private bool hideWall;

        public RoomVisualizationSettingsComposer(int floorThickness, int wallThickness, bool hideWall)
        {
            this.floorThickness = floorThickness;
            this.wallThickness = wallThickness;
            this.hideWall = hideWall;
        }

        public override void Write()
        {
            _data.Add(hideWall);
            _data.Add(wallThickness);
            _data.Add(floorThickness);
        }
    }
}
