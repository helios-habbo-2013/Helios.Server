namespace Helios.Messages.Outgoing
{
    class ChangeNameUpdateComposer : IMessageComposer
    {
        public ChangeNameUpdateComposer()
        {

        }

        public override void Write()
        {
            this._data.Add(4);
            this._data.Add("test");
            this._data.Add(2);
            this._data.Add("test123");
            this._data.Add("test1337");
        }
    }
}
