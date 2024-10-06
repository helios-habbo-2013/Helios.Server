using Helios.Messages;
using Newtonsoft.Json;

namespace Helios.Game
{
    public class StickieInteractor : Interactor
    {
        #region Overridden Properties



        #endregion

        public StickieInteractor(Item item) : base(item) 
        {
            StickieExtraData extraData = null;

            try
            {
                extraData = JsonConvert.DeserializeObject<StickieExtraData>(Item.Data.ExtraData);
            } catch { }

            if (extraData == null)
            {
                extraData = new StickieExtraData
                {
                    Message = string.Empty,
                    Colour = "FFFF33"
                };
            }

            SetExtraData(extraData);
        }

        public override void WriteExtraData(IMessageComposer composer, bool inventoryView = false)
        {
            composer.Data.Add((int)ExtraDataType.StringData);
            composer.Data.Add(GetJsonObject<StickieExtraData>().Colour);
        }
    }
}
