using Helios.Game;

namespace Helios.Messages.Outgoing
{
    class UserObjectComposer : IMessageComposer
    {
        private Avatar avatar;

        public UserObjectComposer(Avatar avatar)
        {
            this.avatar = avatar;
        }

        public override void Write()
        {
            this.AppendStringWithBreak(avatar.Details.Id.ToString());
            this.AppendStringWithBreak(avatar.Details.Name);
            this.AppendStringWithBreak(avatar.Details.Figure);
            this.AppendStringWithBreak(avatar.Details.Sex.ToUpper());
            this.AppendStringWithBreak(avatar.Details.Motto);
            this.AppendStringWithBreak(avatar.Details.RealName);
            this.AppendInt32(0);
            this.AppendStringWithBreak("");
            this.AppendInt32(0);
            this.AppendInt32(0);
            this.AppendInt32(avatar.Settings.Respect);
            this.AppendInt32(avatar.Settings.DailyRespectPoints);
            this.AppendInt32(avatar.Settings.DailyPetRespectPoints);
            /*this.AppendStringWithBreak(true);
            this.AppendStringWithBreak(avatar.Details.PreviousLastOnline.ToString("MM-dd-yyyy HH:mm:ss"));
            this.AppendStringWithBreak(true);
            this.AppendStringWithBreak(false);*/
        }


        public override int HeaderId => 5;
    }
}