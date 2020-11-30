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
