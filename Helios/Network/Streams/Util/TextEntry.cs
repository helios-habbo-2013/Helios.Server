using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Network.Streams.Util
{
    public class TextEntry
    {
        #region Private variables

        private string m_Value;

        #endregion

        #region Fields

        public string Value
        {
            get { return m_Value; }
        }

        #endregion

        #region Constructor

        public TextEntry(object value = null)
        {
            m_Value = value.ToString();
        }

        #endregion
    }
}
