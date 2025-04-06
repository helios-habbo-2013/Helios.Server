using DotNetty.Buffers;
using Helios.Util;
using Helios.Util.Specialised;
using System;

namespace Helios.Network.Streams
{
    public class Response
    {
        #region Fields

        private IByteBuffer m_Buffer;

        #endregion

        #region Properties

        /// <summary>
        /// Get the message header
        /// </summary>
        public int Header { get; private set; }

        /// <summary>
        /// Get the message body with characters replaced
        /// </summary>
        public string MessageBody
        {
            get
            {
                string consoleText = m_Buffer.ToString(StringUtil.GetEncoding());

                for (int i = 0; i < 14; i++)
                    consoleText = consoleText.Replace(Convert.ToString((char)i), "[" + i + "]");

                return consoleText;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for response
        /// </summary>
        /// <param name="header"></param>
        /// <param name="buffer"></param>
        public Response(int header, IByteBuffer buffer)
        {
            this.Header = header;
            this.m_Buffer = buffer;
            this.m_Buffer.WriteBytes(Base64Encoding.EncodeInt32(Header, 2));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Write string for client
        /// </summary>
        /// <param name="obj"></param>
        public void WriteString(object obj)
        {
            m_Buffer.WriteBytes(StringUtil.GetEncoding().GetBytes(obj.ToString()));
            m_Buffer.WriteByte(2);
        }

        /// <summary>
        /// Write raw object to buffer
        /// </summary>
        public void Write(object obj)
        {
            m_Buffer.WriteBytes(StringUtil.GetEncoding().GetBytes(obj.ToString()));
        }

        /// <summary>
        /// Write int for client
        /// </summary>
        /// <param name="obj"></param>
        public void WriteInt(int obj)
        {
            m_Buffer.WriteBytes(WireEncoding.EncodeInt32(obj));
        }

        /// <summary>
        /// Write boolean for client
        /// </summary>
        /// <param name="obj"></param>
        public void WriteBool(bool obj)
        {
            WriteInt(obj ? 1 : 0);
        }

        #endregion
    }
}
