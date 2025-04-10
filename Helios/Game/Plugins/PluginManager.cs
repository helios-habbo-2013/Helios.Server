using System.Linq;
using System.Collections.Generic;
using System.IO;
using Serilog;

namespace Helios.Game
{
    public class PluginManager : ILoadable
    {
        #region Fields

        public static readonly PluginManager Instance = new PluginManager();
        private List<PluginProxy> plugins;
        private readonly string pluginDictionary = "plugins";

        #endregion

        #region Properties

        public IPlugin[] Plugins => [.. plugins.Select(x => x.Plugin)];

        #endregion

        #region Constructors

        public void Load()
        {
            ReloadPlugins();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Reload plugin handler
        /// </summary>
        public void ReloadPlugins()
        {
            bool isReload = plugins != null;

            if (!Directory.Exists(pluginDictionary))
                Directory.CreateDirectory(pluginDictionary);

            if (plugins != null)
            {
                plugins.ForEach(x => x.Plugin.onDisable());
                plugins = null;
            }

            if (plugins == null)
            {
                plugins = new List<PluginProxy>();

                foreach (string file in Directory.GetFiles(pluginDictionary))
                {
                    if (Path.GetExtension(file) != ".dll")
                        continue;

                    Log.ForContext<PluginManager>().Information($"Loading {Path.GetFileName(file)} as a plugin");
                    plugins.Add(new PluginProxy(file));
                }
            }

            plugins.ForEach(x => x.Plugin.onEnable());
        }

        #endregion
    }
}
