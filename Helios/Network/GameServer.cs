using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Serilog;
using System;
using System.Net;
using System.Reflection;

namespace Helios.Network
{
    public class GameServer
    {
        #region Fields

        private static readonly GameServer m_GameServer = new GameServer();

        private readonly MultithreadEventLoopGroup m_BossGroup;
        private readonly MultithreadEventLoopGroup m_WorkerGroup;

        private string _ip;
        private int _port;

        #endregion

        #region Properties

        /// <summary>
        /// Invoke the singleton instance
        /// </summary>
        public static GameServer Instance
        {
            get
            {
                return m_GameServer;
            }
        }

        /// <summary>
        /// Get the server IP address
        /// </summary>
        public string IpAddress
        {
            get { return _ip; }
        }

        /// <summary>
        /// Port
        /// </summary>
        public int Port
        {
            get { return _port; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// GameServer constructor
        /// </summary>
        public GameServer()
        {
            this.m_BossGroup = new MultithreadEventLoopGroup(1);
            this.m_WorkerGroup = new MultithreadEventLoopGroup(10);
        }

        #endregion

        #region Public methods

        public void CreateServer(string ip, int port)
        {
            this._ip = ip;
            this._port = port;
        }

        /// <summary>
        /// Initialise the game server by given pot
        /// </summary>
        /// <param name="port">the game port</param>
        public bool InitialiseServer()
        {
            try
            {
                Log.ForContext<Helios>().Information("Starting server");

                ServerBootstrap bootstrap = new ServerBootstrap()
                    .Group(m_BossGroup, m_WorkerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .ChildHandler(new GameChannelInitializer())
                    .ChildOption(ChannelOption.TcpNodelay, true)
                    .ChildOption(ChannelOption.SoKeepalive, true)
                    .ChildOption(ChannelOption.SoReuseaddr, true)
                    .ChildOption(ChannelOption.SoRcvbuf, 1024)
                    .ChildOption(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024))
                    .ChildOption(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default);

                var bindTask = bootstrap.BindAsync(IPAddress.Parse(_ip), _port);

                bindTask.ContinueWith(task =>
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        Log.ForContext<GameServer>().Information($"Server successfully bound on {_ip}:{_port}");
                    }
                    else
                    {
                        Log.ForContext<GameServer>().Error(task.Exception, "Failed to bind server to port");
                    }
                });

                bindTask.Wait(); // Wait here to ensure exception propagation if needed

                return true;
            }
            catch (Exception ex)
            {
                Log.ForContext<GameServer>().Error(ex, "Failed to setup network listener");

                return false;
            }
        }


        #endregion
    }
}
