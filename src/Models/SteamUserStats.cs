using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteamModels
{
    /// <summary>
    /// Describing generic player stats
    /// GET: http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid=<GAME_ID>&key=<API_KEY>&steamid=<STEAMID64>&format=json
    /// </summary>
    public class SteamUserStats
    {
        /// <summary>
        /// Gets or sets the playerstats.
        /// </summary>
        /// <value>
        /// The playerstats.
        /// </value>
        public PlayerStats playerstats { get; set; }
    }

    public class PlayerStats
    {
        /// <summary>
        /// Gets or sets the player steam identifier.
        /// </summary>
        /// <value>
        /// The steam identifier.
        /// </value>
        public string steamID { get; set; }

        /// <summary>
        /// Gets or sets the name of the game retrieving stats for.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string gameName { get; set; }

        /// <summary>
        /// Gets or sets the player stats.
        /// </summary>
        /// <value>
        /// The player stats.
        /// </value>
        public List<StatDescriptor> stats { get; set; }

        /// <summary>
        /// Gets or sets the achievements.
        /// </summary>
        /// <value>
        /// The achievements.
        /// </value>
        [JsonIgnore]
        public List<AchievementDescriptor> achievements { get; set; }

        /// <summary>
        /// Gets or sets the error returned instead of the stats.
        /// When this is populated <see cref="stats"/> and <see cref="achievements"/> are null.
        /// The Steam API currently signals a private profile with an http 400 and an empty body
        /// rather than with this field, so it is only a fallback for interfaces that still use it.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string error { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request actually returned stats.
        /// An account that owns the game but has no stats for it comes back as an http 200 with a
        /// steamID and a gameName but no stats array at all, which this reports as <c>false</c>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if stats were returned; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool success => error == null && stats != null;
    }

    /// <summary>
    /// Describes steam user stat model
    /// </summary>
    public class AchievementDescriptor
    {
        /// <summary>
        /// Gets or sets the name of the achievement.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the status of the achievement.
        /// </summary>
        /// <value>
        /// The status of the achievement.
        /// </value>
        public int achieved { get; set; }
    }

    /// <summary>
    /// Describes steam user achievement model
    /// </summary>
    public class StatDescriptor
    {
        /// <summary>
        /// Gets or sets the name of the stat.
        /// </summary>
        /// <value>
        /// The name of the stat.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the value of the stat.
        /// </summary>
        /// <value>
        /// The value of the stat.
        /// </value>
        public int value { get; set; }
    }
}
