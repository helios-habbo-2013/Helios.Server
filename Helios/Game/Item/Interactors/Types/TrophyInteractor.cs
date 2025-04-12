using Helios.Messages;
using Helios.Util.Extensions;
using Newtonsoft.Json;
using System.Text;

namespace Helios.Game
{
    public class TrophyInteractor : Interactor
    {
        #region Overridden Properties



        #endregion

        public TrophyInteractor(Item item) : base(item)
        {

        }


        public override void WriteExtraData(IMessageComposer composer, bool inventoryView = false)
        {
            var trophyData = GetJsonObject<TrophyExtraData>();

            StringBuilder builder = new StringBuilder();
            builder.Append(AvatarManager.Instance.GetName(trophyData.AvatarId));
            builder.Append((char)9);
            builder.Append(trophyData.Date.ToDateTime().ToString("dd-MM-yyyy"));
            builder.Append((char)9);
            builder.Append(trophyData.Message.FilterInput(false));

            //composer.Data.Add((int)ExtraDataType.Legacy);
            //composer.Data.Add(builder.ToString());

            composer.AppendStringWithBreak(builder.ToString());
        }
    }
}
