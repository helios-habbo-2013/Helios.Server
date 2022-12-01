using System.Text;

namespace Helios.Util
{
    public class StringUtil
    {
        #region Public methods

        public static Encoding GetEncoding()
        {
            return Encoding.GetEncoding("ISO-8859-1");
        }

        #endregion
    }
}
