namespace Helios.Messages.Outgoing
{
    class ChangeNameUpdateComposer : IMessageComposer
    {
        public ChangeNameUpdateComposer()
        {

        }

        public override void Write()
        {
            this.m_Data.Add(4);
            this.m_Data.Add("test");
            this.m_Data.Add(2);
            this.m_Data.Add("test123");
            this.m_Data.Add("test1337");
        }
    }
}
