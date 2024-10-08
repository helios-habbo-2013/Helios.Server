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


        #endregion

        public GuildInteractor(Item item) : base(item)
        {
            GuildExtraData extraData = null;
            Group group = null;

            bool saveGroup = false;

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
                    group = GroupManager.Instance.GetGroup(item.Data.GroupId.Value);

                    if (group != null)
                    {
                        extraData = new GuildExtraData()
                        {
                            State = "0",
                            Badge = group.Data.Badge,
                            Colour1 = GroupManager.Instance.BadgeManager.Colour2[group.Data.Colour1].FirstValue,
                            Colour2 = GroupManager.Instance.BadgeManager.Colour3[group.Data.Colour2].FirstValue,
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
            if (Item.Data.GroupId != null && group == null)
            {
                Item.Data.GroupId = null;
                saveGroup = true;
            }

            if (saveGroup)
            {
                Item.Save();
            }
        }


        /// <summary>
        /// On interact group guild furni handler
        /// </summary>
        public override void OnInteract(IEntity entity, int requestData)
        {

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
