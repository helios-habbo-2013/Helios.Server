namespace Helios.Messages.Outgoing
{
    class AllowancesComposer : IMessageComposer
    {
        private bool SAFECHAT;
        private bool ISGUIDE;
        private bool VOTE_IN_COMP;

        public AllowancesComposer()
        {
            SAFECHAT = true;
            ISGUIDE = false;
            VOTE_IN_COMP = false;
        }

        public override void Write()
        {
            _data.Add(6);
            _data.Add("SAFE_CHAT");
            _data.Add(SAFECHAT);
            _data.Add((!SAFECHAT) ? "requirement.unfulfilled.safety_quiz_1" : "");
            _data.Add("USE_GUIDE_TOOL");
            _data.Add(ISGUIDE);
            _data.Add((!ISGUIDE) ? "requirement.unfulfilled.helper_level_4" : "");
            _data.Add("VOTE_IN_COMPETITIONS");
            _data.Add(VOTE_IN_COMP);
            _data.Add((!VOTE_IN_COMP) ? "requirement.unfulfilled.helper_level_2" : "");
            _data.Add("TRADE");
            _data.Add(true);
            _data.Add("");
            _data.Add("FULL_CHAT");
            _data.Add(true);
            _data.Add("");
            _data.Add("CITIZEN");
            _data.Add(true);
            _data.Add("");
        }
    }
}
