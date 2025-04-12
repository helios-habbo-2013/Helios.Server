namespace Helios.Messages.Outgoing
{
    class ChangeNameStatusComposer : IMessageComposer
    {
        public ChangeNameStatusComposer()
        {

        }

        public override void Write()
        {
            this._data.Add(0);
            this._data.Add("test");
            this._data.Add(0);
        }

        public override int HeaderId => -1;
    }
}
