using DotNetty.Buffers;
using Helios.Util;
using Helios.Util.Specialised;
using System;

namespace Helios.Network.Streams
{
    public class Request
    {
        #region Fields

        private string m_Header;
        private int m_HeaderId;
        private IByteBuffer m_Buffer;

        #endregion

        /// <summary>
        /// Get the message header
        /// </summary>
        public string Header
        {
            get { return m_Header; }
        }

        /// <summary>
        /// Get the message header id
        /// </summary>
        public int HeaderId
        {
            get { return m_HeaderId; }
        }

        /// <summary>
        /// Get the message buffer
        /// </summary>
        public IByteBuffer Buffer
        {
            get { return m_Buffer; }
        }

        /// <summary>
        /// Get the message body with characters replaced
        /// </summary>
        public string MessageBody
        {
            get
            {
                string consoleText = m_Buffer.ToString(StringUtil.GetEncoding());

                for (int i = 0; i < 13; i++)
                {
                    consoleText = consoleText.Replace(Convert.ToString((char)i), "[" + i + "]");
                }

                return consoleText;
            }
        }

        /// <summary>
        /// Get the readable bytes left
        /// </summary>
        public byte[] ReadableBytes
        {
            get
            {
                m_Buffer.MarkReaderIndex();
                byte[] bytes = new byte[m_Buffer.ReadableBytes];
                m_Buffer.ReadBytes(bytes);
                m_Buffer.ResetReaderIndex();
                return bytes;
            }
        }

        /// <summary>
        /// Get the content of the packet
        /// </summary>
        public string Content
        {
            get
            {
                return StringUtil.GetEncoding().GetString(ReadableBytes);
            }
        }

        #region Constructors

        /// <summary>
        /// Constructor for request
        /// </summary>
        /// <param name="length"></param>
        /// <param name="buffer"></param>
        public Request(IByteBuffer buffer)
        {
            this.m_Buffer = buffer;
            this.m_Header = StringUtil.GetEncoding().GetString(new byte[] { buffer.ReadByte(), buffer.ReadByte() });
            this.m_HeaderId = Base64Encoding.DecodeInt32(StringUtil.GetEncoding().GetBytes(this.m_Header));
        }

        #endregion

        /// <summary>
        /// Read VL64 integer
        /// </summary>
        /// <returns>the integer from client</returns>
        public int ReadInt()
        {
            try
            {
                if (ReadableBytes.Length == 0)
                    return 0;

                byte[] bzData = this.ReadableBytes;
                int totalBytes = 0;
                int i = WireEncoding.DecodeInt32(bzData, out totalBytes);
                this.ReadBytes(totalBytes);

                return i;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Read B64 integer
        /// </summary>
        /// <returns>the integer from client</returns>
        public int ReadBase64Int()
        {
            try
            {
                if (ReadableBytes.Length < 2)
                    return 0;

                int i = Base64Encoding.DecodeInt32(this.ReadBytes(2));
                return i;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Read base64 integer
        /// </summary>
        /// <returns>the integer from client</returns>
        public string ReadString()
        {
            try
            {
                if (ReadableBytes.Length < 2)
                    return null;

                int totalBytes = Base64Encoding.DecodeInt32(this.ReadBytes(2));

                if (ReadableBytes.Length < totalBytes)
                    return null;

                string value = StringUtil.GetEncoding().GetString(this.ReadBytes(totalBytes));
                return value;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Read boolean
        /// </summary>
        /// <returns>the boolean
        /// from client</returns>
        public bool ReadBool()
        {
            try
            {
                return m_Buffer.ReadByte() == 'I';
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Read bytes
        /// </summary>
        /// <returns>the bytes from client</returns>
        public byte[] ReadBytes(int len)
        {
            try
            {
                byte[] payload = new byte[len];
                m_Buffer.ReadBytes(payload);
                return payload;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Dispose handler of request
        /// </summary>
        public void Dispose()
        {
            m_Buffer.Release();
        }
    }
}
