using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Network.Streams.Util
{
    public class ValueEntry
    {
        #region Private variables

        private string m_Value;
        private string m_Delimiter;

        #endregion

        #region Fields

        public string Value
        {
            get { return m_Value; }
        }

        public string Delimiter
        {
            get { return m_Delimiter; }
        }

        #endregion

        #region Constructor

        public ValueEntry(object value = null, object delimeter = null)
        {
            m_Value = (value == null ? string.Empty : value).ToString();
            m_Delimiter = (delimeter == null ? (char)9 : delimeter).ToString();
        }

        #endregion
    }
}
