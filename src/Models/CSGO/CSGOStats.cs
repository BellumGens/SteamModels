using System;
using System.Collections.Generic;

namespace SteamModels.CSGO
{
    /// <summary>
    /// View Model describing player statistics in CS:GO
    /// </summary>
    public class CSGOPlayerStats : PlayerStats
    {
        private List<StatDescriptor> _killDeathStats;
        private decimal _killDeathRatio = -1;
        private decimal _headshotPercentage = -1;
        private decimal _accuracy = -1;

        /// <summary>
        /// The stat names
        /// </summary>
        private static Dictionary<string, string> _statNames = new Dictionary<string, string>()
        {
            { "total_kills", "Total Kills" },
            { "total_deaths", "Total Deaths" },
            { "total_kills_headshot", "Total Headshots" },
            { "total_shots_fired", "Total Shots" },
            { "total_shots_hit", "Total Hits" }
        };

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
                    foreach (StatDescriptor stat in stats)
                    {
                        if (_statNames.ContainsKey(stat.name)) 
                        {
                            stat.name = _statNames[stat.name];
                            _killDeathStats.Add(stat);
                        }
                    }
                }
                return _killDeathStats;
            }
        }

        /// <summary>
        /// Gets the player kill death ratio, if total kills and total deaths have been populated. Otherwise -1 is returned.
        /// </summary>
        /// <value>
        /// The kill death ratio, if total kills and total deaths have been populated. Otherwise -1 is returned.
        /// </value>
        public decimal KillDeathRatio
        {
            get
            {
                decimal kills = 1, deaths = -1;
                if (_killDeathRatio == -1)
                {
                    foreach (StatDescriptor stat in KillDeathStats)
                    {
                        if (stat.name == "Total Kills")
                            kills = stat.value;
                        if (stat.name == "Total Deaths")
                            deaths = stat.value;
                    }
                    _killDeathRatio = kills / deaths;
                }
                return Math.Round(_killDeathRatio, 2);
            }
        }

        /// <summary>
        /// Gets or sets the headshot percentage.
        /// </summary>
        /// <value>
        /// The headshot percentage.
        /// </value>
        public decimal HeadshotPercentage
        {
            get
            {
                decimal kills = 1, headshots = -1;
                if (_headshotPercentage == -1)
                {
                    foreach (StatDescriptor stat in KillDeathStats)
                    {
                        if (stat.name == "Total Kills")
                            kills = stat.value;
                        if (stat.name == "Total Headshots")
                            headshots = stat.value;
                    }
                    _headshotPercentage = headshots / kills * 100;
                }
                return Math.Round(_headshotPercentage, 2);
            }    
        }

        /// <summary>
        /// Gets the overal accuracy percentage.
        /// </summary>
        /// <value>
        /// The overal accuracy percentage.
        /// </value>
        public decimal Accuracy
        {
            get
            {
                decimal shots = 1, hits = -1;
                if (_accuracy == -1)
                {
                    foreach (StatDescriptor stat in KillDeathStats)
                    {
                        if (stat.name == "Total Shots")
                            shots = stat.value;
                        if (stat.name == "Total Hits")
                            hits = stat.value;
                    }
                    _accuracy = hits / shots * 100;
                }
                return Math.Round(_accuracy, 2);
            }
        }
    }
}
