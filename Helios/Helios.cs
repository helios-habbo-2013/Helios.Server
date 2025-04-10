using Helios.Util;
using System;
using System.IO;
using System.Reflection;
using Helios.Network;
using Helios.Game;
using Helios.Messages;
using System.Threading;
using Helios.Storage.Access;
using System.Linq;
using Newtonsoft.Json;
using Serilog;
using Microsoft.Extensions.Configuration;
using Helios.Storage;

namespace Helios
{
    class Helios
    {
        #region Fields

        private static IConfigurationRoot _configuration;

        #endregion

        #region Properties

        /// <summary>
        /// Get the official release supported
        /// </summary>
        public static string ClientVersion
        {
            get { return "RELEASE63-201302211227-193109692"; }
        }

        public static IConfiguration Configuration => _configuration;

        #endregion

        public static void Boot()
        {
            _configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            Console.Title = "Helios - Habbo Hotel Emulation";

            Log.ForContext<Helios>().Information("Starting server for " + Environment.UserName + "...");
            Log.ForContext<Helios>().Information("Checking for appsettings.json");
            Log.ForContext<Helios>().Information("");

            string sqlConfigLocation = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            if (File.Exists(sqlConfigLocation) == false)
            {
                Log.ForContext<Helios>().Error("appsettings.json not found at " + sqlConfigLocation);
                return;
            }

            Log.ForContext<Helios>().Information("appsettings.json found at " + sqlConfigLocation);
            Log.ForContext<Helios>().Information("");

            try
            {
                if (!TryDatabaseConnection())
                {
                    return;
                }

                if (!TryGameData())
                {
                    return;
                }

                if (!TryCreateServer())
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                Log.ForContext<Helios>().Error(ex, "An exception occurred when booting Helios");
            }

#if DEBUG
            Console.Read();
#endif

        }

        #region Private methods

        /// <summary>
        /// Test database connection
        /// </summary>
        private static bool TryDatabaseConnection()
        {
            try
            {
                Log.ForContext<StorageContext>().Information("Attempting to connect to MySQL database");

                using var context = new StorageContext();
                context.Database.EnsureCreated();

                Log.ForContext<StorageContext>().Information("Connection to database is successful!");
            }
            catch (Exception ex)
            {
                Log.ForContext<StorageContext>().Error(ex, "An exception occurred attempting to connect to the database");
                return false;
            }

            Log.ForContext<Helios>().Information("");
            return true;
        }

        /// <summary>
        /// Load game data
        /// </summary>
        private static bool TryGameData()
        {
            try
            {
                PermissionsManager.Instance.Load();
                FuserightManager.Instance.Load();

                ValueManager.Instance.Load();
                Log.ForContext<Helios>().Information("");

                InteractionManager.Instance.Load();
                ItemManager.Instance.Load();
                
                Log.ForContext<Helios>().Information("");

                CatalogueManager.Instance.Load();
                Log.ForContext<Helios>().Information("");

                RoomManager.Instance.Load();
                Log.ForContext<Helios>().Information("");

                SubscriptionManager.Instance.Load();
                Log.ForContext<Helios>().Information("");

                NavigatorManager.Instance.Load();
                Log.ForContext<Helios>().Information("");

                MessageHandler.Instance.Load();
                Log.ForContext<Helios>().Information("");

                PluginManager.Instance.Load();
                GroupManager.Instance.Load();
            }
            catch (Exception ex)
            {
                Log.ForContext<Helios>().Error(ex, "An exception occurred when loading gamedata");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Try and create server
        /// </summary>
        private static bool TryCreateServer()
        {
            GameServer.Instance.CreateServer("0.0.0.0", 30001); //ServerConfig.Instance.GetString("server", "ip"), ServerConfig.Instance.GetInt("server", "port"));
            return GameServer.Instance.InitialiseServer();
        }

        #endregion
    }
}
