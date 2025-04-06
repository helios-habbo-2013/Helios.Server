using Helios.Game;

namespace Helios.Messages.Outgoing
{
    public class UserInfoComposer : IMessageComposer
    {
        private Avatar avatar;

        public UserInfoComposer(Avatar avatar)
        {
            this.avatar = avatar;
        }

        public override void Write()
        {
            _data.Add(avatar.Details.Id.ToString());
            _data.Add(avatar.Details.Name);
            _data.Add(avatar.Details.Figure);
            _data.Add(avatar.Details.Sex.ToUpper());
            _data.Add(avatar.Details.Motto);
            _data.Add(avatar.Details.RealName);
            _data.Add(false);
            _data.Add("");
            _data.Add(0);
            _data.Add(0);
            _data.Add(avatar.Settings.Respect);
            _data.Add(avatar.Settings.DailyRespectPoints);
            _data.Add(avatar.Settings.DailyPetRespectPoints);
            /*_data.Add(true);
            _data.Add(avatar.Details.PreviousLastOnline.ToString("MM-dd-yyyy HH:mm:ss"));
            _data.Add(true);
            _data.Add(false);*/
        }
    }
}
