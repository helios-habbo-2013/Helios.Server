namespace Helios.Messages.Outgoing
{
    class FurnitureAliasesComposer : IMessageComposer
    {
        public override void Write()
        {
            _data.Add(0);
        }

        public int HeaderId => -1;
    }
}
