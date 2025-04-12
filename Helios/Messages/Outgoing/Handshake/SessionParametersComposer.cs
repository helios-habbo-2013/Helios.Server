using Helios.Game;
using Helios.Network.Streams;
using System.Collections.Generic;
using System.Numerics;

namespace Helios.Messages.Outgoing
{
    public class SessionParametersComposer : IMessageComposer
    {
        public enum SessionParamType
        {
            RegisterCoppa = 0,
            VoucherEnabled = 1,
            RegisterRequireParentEmail = 2,
            RegisterSendParentEmail = 3,
            AllowDirectMail = 4,
            DateFormat = 5,
            PartnerIntegrationEnabled = 6,
            AllowProfileEditing = 7,
            TrackingHeader = 8,
            TutorialEnabled = 9
        }

        public Avatar avatar;

        public SessionParametersComposer(Avatar avatar)
        {
            this.avatar = avatar;
        }

        public override void Write()
        {
            var parameters = new Dictionary<SessionParamType, string>();

            parameters[SessionParamType.RegisterCoppa] = "0";
            parameters[SessionParamType.VoucherEnabled] = "1"; // GameConfiguration.Instance.GetBoolean("vouchers.enabled") ? "1" : "0";
            parameters[SessionParamType.RegisterSendParentEmail] = "0";
            parameters[SessionParamType.RegisterRequireParentEmail] = "1";
            parameters[SessionParamType.AllowDirectMail] = "0";
            parameters[SessionParamType.DateFormat] = "dd-MM-yyyy";
            parameters[SessionParamType.PartnerIntegrationEnabled] = "0";
            parameters[SessionParamType.AllowProfileEditing] = "0"; // GameConfiguration.Instance.GetBoolean("profile.editing") ? "1" : "0";
            parameters[SessionParamType.TrackingHeader] = "hotel-co.uk";
            parameters[SessionParamType.TutorialEnabled] = "0"; //IsTutorialEnabled(avatar) ? "1" : "0";

            _data.Add(parameters.Count);

            foreach (var entry in parameters)
            {
                _data.Add((int)entry.Key);

                if (!string.IsNullOrEmpty(entry.Value) && char.IsDigit(entry.Value[0]))
                {
                    if (int.TryParse(entry.Value, out int intValue))
                    {
                        _data.Add(intValue);
                    }
                    else
                    {
                        _data.Add(entry.Value);
                    }
                }
                else
                {
                    _data.Add(entry.Value);
                }
            }
        }

        private bool IsTutorialEnabled(Avatar avatar)
        {
            return true;//GameConfiguration.Instance.GetBoolean("tutorial.enabled");
        }

        public override int HeaderId => 257;
    }
}
