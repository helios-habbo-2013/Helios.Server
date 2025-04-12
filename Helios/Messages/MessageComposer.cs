using Helios.Network.Streams.Util;
using System.Collections.Generic;

namespace Helios.Messages
{
    public abstract class IMessageComposer
    {
        protected List<object> _data;
        protected bool _composed;
        
        public virtual int HeaderId { get; }

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

        public IMessageComposer AppendStringWithBreak(string str)
        {
            _data.Add(str);
            return this;
        }

        public IMessageComposer AppendInt32(int i)
        {
            _data.Add(i);
            return this;
        }

        public IMessageComposer AppendString(string str)
        {
            _data.Add(new TextEntry(str));
            return this;
        }

        public IMessageComposer AppendBoolean(bool b)
        {
            _data.Add(b);
            return this;
        }
    }
}