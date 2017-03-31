using System;
using System.Collections.Generic;

namespace SteamModels
{
    /// <summary>
    /// Player Stats
    /// </summary>
    public class CSGOStats
    {
        public CSGOPlayerStats playerstats { get; set; }
    }

    /// <summary>
    /// View Model describing player statistics in CS:GO
    /// </summary>
    public class CSGOPlayerStats
    {
        private List<StatDescriptor> _killDeathStats;
        private decimal _killDeathRatio = -1;
        /// <summary>
        /// Gets or sets the steam identifier.
        /// </summary>
        /// <value>
        /// The steam identifier.
        /// </value>
        public string SteamId { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        /// <value>
        /// The name of the player.
        /// </value>
        public string GameName { get; set; }

        /// <summary>
        /// Gets or sets the player stats.
        /// </summary>
        /// <value>
        /// The player stats.
        /// </value>
        public List<StatDescriptor> Stats { get; set; }

        /// <summary>
        /// Gets or sets the achievements.
        /// </summary>
        /// <value>
        /// The achievements.
        /// </value>
        public List<AchievementDescriptor> Achievements { get; set; }

        /// <summary>
        /// Gets or sets the kill death stats.
        /// </summary>
        /// <value>
        /// The kill death stats.
        /// </value>
        public List<StatDescriptor> KillDeathStats
        {
            get
            {
                if (_killDeathStats == null)
                {
                    _killDeathStats = new List<StatDescriptor>();
                    foreach (StatDescriptor stat in Stats)
                    {
                        if (stat.Name == "total_kills")
                        {
                            stat.Name = "Total Kills";
                            _killDeathStats.Add(stat);
                        }
                        else if (stat.Name == "total_deaths")
                        {
                            stat.Name = "Total Deaths";
                            _killDeathStats.Add(stat);
                        }
                    }
                }
                return _killDeathStats;
            }
        }

        public string KillDeathRatio
        {
            get
            {
                decimal kills = 1, deaths = -1;
                foreach (StatDescriptor stat in KillDeathStats)
                {
                    if (stat.Name == "Total Kills")
                        kills = stat.Value;
                    if (stat.Name == "Total Deaths")
                        deaths = stat.Value;
                }
                _killDeathRatio = kills / deaths;
                return "K/D Ratio:" + Math.Round(_killDeathRatio, 2);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AchievementDescriptor
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the achieved.
        /// </summary>
        /// <value>
        /// The achieved.
        /// </value>
        public int Achieved { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StatDescriptor
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Name { get; set; }
    }
}
