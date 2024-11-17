using Helios.Messages;
using Helios.Storage.Access;
using Helios.Storage.Models.Group;
using Helios.Util.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helios.Game
{
    public class GuildInteractor : Interactor
    {
        #region Overridden Properties

        private DefaultInteractor DefaultInteractor { get; set; }

        #endregion

        public GuildInteractor(Item item) : base(item)
        {
            bool saveGroup = false;
            bool hasGroup = GroupManager.Instance.HasGroup(item.Data.GroupId.Value);

            GuildExtraData extraData = null;

            try
            {
                extraData = JsonConvert.DeserializeObject<GuildExtraData>(Item.Data.ExtraData);
            }
            catch { }

            if (extraData == null || extraData.Badge == null)
            {
                extraData = null;

                if (Item.Data.GroupId != null)
                {
                    if (hasGroup)
                    {
                        Group group = GroupManager.Instance.GetGroup(item.Data.GroupId.Value);

                        extraData = new GuildExtraData()
                        {
                            State = "0",
                            Badge = group.Data.Badge,
                            Colour1 = group.ColourA,
                            Colour2 = group.ColourB,
                        };

                        saveGroup = true;
                    }

                }
            }

            if (extraData == null || item.Data.GroupId == null)
            {
                extraData = new GuildExtraData()
                {
                    State = "0",
                    Badge = null,
                    Colour1 = null,
                    Colour2 = null,
                };

                saveGroup = true;
            }

            SetExtraData(extraData);

            // Sanity checking
            if (Item.Data.GroupId != null && !hasGroup)
            {
                Item.Data.GroupId = null;
                saveGroup = true;
            }

            if (saveGroup)
            {
                Item.Save();
            }

            this.DefaultInteractor = new DefaultInteractor(item);
        }

        /// <summary>
        /// On interact group guild furni handler
        /// </summary>
        public override void OnInteract(IEntity entity, int requestData)
        {
            var currentState = GetJsonObject<GuildExtraData>().State;

            if (string.IsNullOrEmpty(currentState) || currentState == "0")
            {
                if (Item.Definition.InteractorType == InteractorType.GATE ||
                    Item.Definition.InteractorType == InteractorType.GUILD_GATE)
                {
                    var roomTile = Item.CurrentTile;

                    if (roomTile != null && roomTile.Entities.Count > 0)
                    {
                        return;
                    }
                }

                if (Item.Definition.Data.MaxStatus > 0)
                {
                    int.TryParse(currentState, out int currentMode);

                    int newMode = currentMode + 1;

                    if (newMode >= Item.Definition.Data.MaxStatus)
                        newMode = 0;

                    Item.UpdateState(newMode.ToString());
                    Item.Save();
                }
            }
        }

        public override void WriteExtraData(IMessageComposer composer, bool inventoryView = false)
        {
            var stringValues = new List<string>();

            var guildFurniData = GetJsonObject<GuildExtraData>();

            if (Item.Data.GroupId != null)
            {
                stringValues.Add(guildFurniData.State);
                stringValues.Add(Item.Data.GroupId.ToString());
                stringValues.Add(guildFurniData.Badge);
                stringValues.Add(guildFurniData.Colour1);
                stringValues.Add(guildFurniData.Colour2);
            }
            
            composer.Data.Add((int)ExtraDataType.StringArray);
            composer.Data.Add(stringValues.Count);

            stringValues.ForEach(x => composer.Data.Add(x));
        }
    }
}
