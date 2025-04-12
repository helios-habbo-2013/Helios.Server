using Helios.Game;

namespace Helios.Messages.Outoing
{
    internal class RoomSettingsDataComposer : IMessageComposer
    {
        private Room room;

        public RoomSettingsDataComposer(Room room)
        {
            this.room = room;
        }

        public override void Write()
        {
            _data.Add(room.Data.Id);
            _data.Add(room.Data.Name);
            _data.Add(room.Data.Description);
            _data.Add((int)room.Data.Status);
            _data.Add(room.Data.Category.Id);
            _data.Add(room.Data.UsersMax);
            _data.Add(((room.Model.MapSizeX * room.Model.MapSizeY) > 100) ? 50 : 25); // what the fuck is this??

            _data.Add(room.Data.Tags.Count);

            foreach (var tag in room.Data.Tags)
            {
                _data.Add(tag.Text);
            }

            _data.Add(room.Data.TradeSetting);
            _data.Add(room.Data.AllowPets ? 1 : 0);
            _data.Add(room.Data.AllowPetsEat ? 1 : 0);
            _data.Add(room.Data.AllowWalkthrough ? 1 : 0);
            _data.Add(room.Data.IsHidingWall ? 1 : 0);
            _data.Add(room.Data.WallThickness);
            _data.Add(room.Data.FloorThickness);
            _data.Add((int)room.Data.WhoCanMute);
            _data.Add((int)room.Data.WhoCanKick);
            _data.Add((int)room.Data.WhoCanBan);
            //m_Data.Add(room.Data.AllowPets ? 1 : 0); 
            //m_Data.Add(room.Data.AllowPetsEat ? 1 : 0);
            //m_Data.Add(room.Data.AllowWalkthrough ? 1 : 0);
            //m_Data.Add(room.Data.IsHidingWall ? 1 : 0);
            //m_Data.Add(room.Data.WallThickness);
            //m_Data.Add(room.Data.FloorThickness);
        }

        public int HeaderId => -1;
    }
}