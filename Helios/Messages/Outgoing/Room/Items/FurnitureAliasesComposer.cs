namespace Helios.Messages.Outgoing
{
    class FurnitureAliasesComposer : IMessageComposer
    {
        public override void Write()
        {
            _data.Add(0);
        }

        public override int HeaderId => 297;
    }
}
