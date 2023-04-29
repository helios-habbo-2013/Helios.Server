using System;
using DotNetty.Transport.Channels;
using Helios.Game;
using Helios.Messages;

namespace Helios.Network.Session
{
    public class ConnectionSession
    {
        #region Fields

        private bool m_Disconnected;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the avatar channel.
        /// </summary>
        public IChannel Channel { get; private set; }

        /// <summary>
        /// Get the ip address of the avatar connected.
        /// </summary>
        public string IpAddress => Channel.RemoteAddress.ToString().Split(':')[3].Replace("]", "");

        /// <summary>
        /// Get avatar instance
        /// </summary>
        public Avatar Avatar { get; private set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for avatar.
        /// </summary>
        /// <param name="channel">the channel</param>
        public ConnectionSession(IChannel channel)
        {
            Channel = channel;
            Avatar = new Avatar(this);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Send message composer
        /// </summary>
        public void Send(IMessageComposer composer)
        {
            if (!composer.Composed)
            {
                composer.Composed = true;
                composer.Write();
            }

            try
            {
                Channel.WriteAndFlushAsync(composer)
                    .RunSynchronously();
            }
            catch { }
        }

        /// <summary>
        /// Kick handler
        /// </summary>
        public void Close()
        {
            Channel.CloseAsync();
        }

        /// <summary>
        /// Disconnection handler
        /// </summary>
        public virtual void Disconnect()
        {
            if (m_Disconnected)
                return;

            m_Disconnected = true;
            Avatar.Disconnect();
        }

        #endregion

    }
}
