namespace Helios.Messages.Outgoing
{
    class ChangeNameStatusComposer : IMessageComposer
    {
        public ChangeNameStatusComposer()
        {

        }

        public override void Write()
        {
            this.m_Data.Add(0);
            this.m_Data.Add("test");
            this.m_Data.Add(0);
        }
    }
}
