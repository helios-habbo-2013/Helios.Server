using System.Linq;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using Serilog;
using System;

namespace Helios.Game
{
    public class FuserightManager : ILoadable
    {
        public class Root
        {
            [YamlMember(Alias = "fuserights")]
            public UserRights Fuserights { get; set; }
        }

        public class UserRights
        {
            [YamlMember(Alias = "ranks")]
            public Dictionary<string, List<string>> Ranks { get; set; }
        }

        #region Fields

        public static readonly FuserightManager Instance = new FuserightManager();

        #endregion

        #region Properties

        public Dictionary<int, List<string>> FuserightRanks;

        #endregion

        #region Constructors

        public FuserightManager() { }

        #endregion

        #region Public methods

        public void Load()
        {
            FuserightRanks = new Dictionary<int, List<string>>();

            Log.ForContext<PermissionsManager>().Information("Loading Fuserights");


            var input = new StringReader(File.ReadAllText("fuserights.yml"));
            var deserializer = new DeserializerBuilder().Build();

            var config = deserializer.Deserialize<Root>(input);

            // Sort the rank keys numerically
            var sortedRanks = config.Fuserights.Ranks
                .OrderBy(kvp => int.Parse(kvp.Key))
                .ToList();

            var inheritedRights = new List<string>();

            foreach (var kvp in sortedRanks)
            {
                int rankId = int.Parse(kvp.Key);
                var currentRights = kvp.Value;

                // Create a new list with inherited + current rights, avoiding duplicates
                var combinedRights = inheritedRights
                    .Union(currentRights)
                    .ToList();

                FuserightRanks[rankId] = combinedRights;

                // Update the inherited list for the next rank
                inheritedRights = combinedRights;
            }

            Log.ForContext<PermissionsManager>().Information("Loaded {Count} of Fuserights", FuserightRanks.Count);
            Log.ForContext<Helios>().Information("");
        }

        #endregion

        #region Public methods

        public bool HasRight(int rankId, string fuseRight)
        {
            return FuserightRanks.TryGetValue(rankId, out List<string> value) && value.Contains(fuseRight);
        }

        public List<string> GetRights(int rankId)
        {
            return FuserightRanks.TryGetValue(rankId, out List<string> value) ? [.. value] : [];
        }

        #endregion
    }
}
