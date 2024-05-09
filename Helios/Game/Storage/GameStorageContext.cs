using Helios.Storage;
using Helios.Storage.Access;
using Helios.Util;
using Helios.Util.Extensions;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Helios.Game
{
    public class GameStorageContext : StorageContext
    {
        #region Properties

       public static DbContextOptionsBuilder<StorageContext> DbContextOptions
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<StorageContext>();
                optionsBuilder.UseMySQL(BuildConnectionString());
                return optionsBuilder;
            }
        }

        #endregion

        #region Constructors

        public GameStorageContext() : base(DbContextOptions.Options)
        {

        }

        #endregion

        #region Methods

        private static string BuildConnectionString()
        {
            var builder = new MySqlConnectionStringBuilder();

            builder.Server = ServerConfig.Instance.GetString("mysql", "hostname");
            builder.Port = (uint) ServerConfig.Instance.GetInt("mysql", "port");
            builder.UserID = ServerConfig.Instance.GetString("mysql", "username");
            builder.Password = ServerConfig.Instance.GetString("mysql", "password");
            builder.Database = ServerConfig.Instance.GetString("mysql", "database");
            builder.MinimumPoolSize = (uint) ServerConfig.Instance.GetInt("mysql", "mincon");
            builder.MaximumPoolSize = (uint) ServerConfig.Instance.GetInt("mysql", "maxcon");

            return builder.ConnectionString;
        }

        #endregion
    }
}
