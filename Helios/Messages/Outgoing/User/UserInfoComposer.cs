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
            m_Data.Add(avatar.Details.Id);
            m_Data.Add(avatar.Details.Name);
            m_Data.Add(avatar.Details.Figure);
            m_Data.Add(avatar.Details.Sex.ToUpper());
            m_Data.Add(avatar.Details.Motto);
            m_Data.Add(avatar.Details.RealName);
            m_Data.Add(false);
            m_Data.Add(avatar.Settings.Respect);
            m_Data.Add(avatar.Settings.DailyRespectPoints);
            m_Data.Add(avatar.Settings.DailyPetRespectPoints);
            m_Data.Add(true);
            m_Data.Add(avatar.Details.PreviousLastOnline.ToString("MM-dd-yyyy HH:mm:ss"));
            m_Data.Add(true);
            m_Data.Add(false);
        }
    }
}
