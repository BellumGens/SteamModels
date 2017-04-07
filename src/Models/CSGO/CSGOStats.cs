using System;
using System.Collections.Generic;

namespace SteamModels
{
    /// <summary>
    /// View Model describing player statistics in CS:GO
    /// </summary>
    public class CSGOPlayerStats : PlayerStats
    {
        private List<StatDescriptor> _killDeathStats;
        private decimal _killDeathRatio = -1;

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
                        if (stat.name == "total_kills")
                        {
                            stat.name = "Total Kills";
                            _killDeathStats.Add(stat);
                        }
                        else if (stat.name == "total_deaths")
                        {
                            stat.name = "Total Deaths";
                            _killDeathStats.Add(stat);
                        }
                    }
                }
                return _killDeathStats;
            }
        }

        public decimal KillDeathRatio
        {
            get
            {
                decimal kills = 1, deaths = -1;
                foreach (StatDescriptor stat in KillDeathStats)
                {
                    if (stat.name == "Total Kills")
                        kills = stat.value;
                    if (stat.name == "Total Deaths")
                        deaths = stat.value;
                }
                _killDeathRatio = kills / deaths;
                return Math.Round(_killDeathRatio, 2);
            }
        }
    }
}
