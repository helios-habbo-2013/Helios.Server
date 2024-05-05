using System.Collections.Generic;

namespace Helios.Messages
{
    public abstract class IMessageComposer
    {
        protected List<object> _data;
        protected bool _composed;

        /// <summary>
        /// Get the data appended
        /// </summary>
        public List<object> Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Get whether the packet is composed
        /// </summary>
        public bool Composed
        {
            get { return _composed; }
            set { _composed = value; }
        }

        public IMessageComposer()
        {
            _data = new List<object>();
        }

        public abstract void Write();

        public object[] GetMessageArray()
        {
            return _data.ToArray();
        }
    }
}