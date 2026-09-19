using System;
using System.Collections.Generic;
using System.Linq;

namespace SteamModels.CSGO
{
    /// <summary>
    /// View Model describing player statistics in CS:GO / Counter-Strike 2.
    /// Counter-Strike 2 kept appid 730 and the legacy CS:GO stat schema, so the same model applies to both.
    /// GET: http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid=730&key=<API_KEY>&steamid=<STEAMID64>&format=json
    /// </summary>
    public class CSGOPlayerStats : SteamUserStats
    {
        private decimal _killDeathRatio = 0;
        private decimal _headshotPercentage = 0;
        private decimal _accuracy = 0;
        private List<WeaponDescriptor> _weapons;
		private WeaponDescriptor _favWeapon;

        /// <summary>
        /// The "total_kills_" stats that do not describe a weapon and are therefore
        /// never considered when picking the <see cref="favouriteWeapon"/>.
        /// </summary>
        private static readonly string[] _nonWeaponKillStats =
        {
            "headshot",
            "enemy_weapon",
            "zoomed_sniper",
            "enemy_blinded",
            "knife_fight"
        };

        /// <summary>
        /// The stat names
        /// </summary>
        //private static Dictionary<string, string> _statNames = new Dictionary<string, string>()
        //{
        //    { "total_kills", "Total Kills" },
        //    { "total_deaths", "Total Deaths" },
        //    { "total_kills_headshot", "Total Headshots" },
        //    { "total_shots_fired", "Total Shots" },
        //    { "total_shots_hit", "Total Hits" }
        //};

        /// <summary>
        /// Gets the value of a single stat, or <paramref name="fallback"/> when the stats
        /// have not been returned at all, e.g. for a private profile.
        /// </summary>
        /// <param name="name">The name of the stat.</param>
        /// <param name="fallback">The value to return when the stat is missing.</param>
        /// <returns>The value of the stat, or <paramref name="fallback"/>.</returns>
        private decimal GetStat(string name, decimal fallback)
        {
            StatDescriptor stat = playerstats?.stats?.FirstOrDefault(s => s.name == name);
            return stat != null ? stat.value : fallback;
        }

        /// <summary>
        /// Divides <paramref name="dividend"/> by <paramref name="divisor"/>, returning 0 when the divisor is 0.
        /// </summary>
        private static decimal Ratio(decimal dividend, decimal divisor)
        {
            return divisor == 0 ? 0 : dividend / divisor;
        }

        /// <summary>
        /// Gets the player kill death ratio. 0 is returned when the stats are not available.
        /// </summary>
        /// <value>
        /// The kill death ratio.
        /// </value>
        public decimal killDeathRatio
        {
            get
            {
                if (_killDeathRatio == 0)
                {
                    _killDeathRatio = Ratio(GetStat("total_kills", 0), GetStat("total_deaths", 1));
                }
                return Math.Round(_killDeathRatio, 2);
            }
        }

        /// <summary>
        /// Gets the headshot percentage. 0 is returned when the stats are not available.
        /// </summary>
        /// <value>
        /// The headshot percentage.
        /// </value>
        public decimal headshotPercentage
        {
            get
            {
                if (_headshotPercentage == 0)
                {
                    _headshotPercentage = Ratio(GetStat("total_kills_headshot", 0), GetStat("total_kills", 1)) * 100;
                }
                return Math.Round(_headshotPercentage, 2);
            }    
        }

        /// <summary>
        /// Gets the overal accuracy percentage. 0 is returned when the stats are not available.
        /// </summary>
        /// <value>
        /// The overal accuracy percentage.
        /// </value>
        public decimal accuracy
        {
            get
            {
                if (_accuracy == 0)
                {
                    _accuracy = Ratio(GetStat("total_shots_hit", 0), GetStat("total_shots_fired", 1)) * 100;
                }
                return Math.Round(_accuracy, 2);
            }
        }

        /// <summary>
        /// Gets the per weapon breakdown, built from the "total_kills_", "total_shots_" and "total_hits_" stats.
        /// An empty list is returned when the stats are not available.
        /// </summary>
        /// <value>
        /// The per weapon breakdown.
        /// </value>
        public List<WeaponDescriptor> weapons {
            get
            {
                if (_weapons == null)
                {
                    _weapons = new List<WeaponDescriptor>();
                    List<StatDescriptor> allStats = playerstats?.stats;
                    if (allStats == null)
                    {
                        return _weapons;
                    }

                    List<StatDescriptor> stats = allStats.Where(s => s.name != null && s.name.StartsWith("total_kills_")).ToList();
                    foreach (StatDescriptor stat in stats)
                    {
                        StatDescriptor shots = allStats.FirstOrDefault(s => s.name == stat.name.Replace("kills", "shots"));
                        StatDescriptor hits = allStats.FirstOrDefault(s => s.name == stat.name.Replace("kills", "hits"));
                        _weapons.Add(new WeaponDescriptor()
                        {
                            name = stat.name.Replace("total_kills_", ""),
                            kills = stat.value,
                            shots = shots != null ? shots.value : 1,
                            hits = hits != null ? hits.value : 0
                        });
                    }
                }
                return _weapons;
            }
        }

        /// <summary>
        /// Gets the favourite weapon, i.e. the weapon with the most kills.
        /// </summary>
        /// <value>
        /// The favourite weapon, or null when the stats are not available.
        /// </value>
        public WeaponDescriptor favouriteWeapon
        {
            get
            {
				if (_favWeapon == null && weapons != null)
				{
					_favWeapon = weapons.Where(w => w.name != null &&
													!_nonWeaponKillStats.Any(n => w.name.Contains(n)))
										.OrderByDescending(w => w.kills)
										.FirstOrDefault();
				}
                return _favWeapon;
            }
        }
    }

    /// <summary>
    /// Describes the kills, shots and hits a player has recorded with a single weapon.
    /// </summary>
    public class WeaponDescriptor
    {
        /// <summary>
        /// Gets or sets the name of the weapon, e.g. "ak47".
        /// </summary>
        /// <value>
        /// The name of the weapon.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the kills recorded with the weapon.
        /// </summary>
        /// <value>
        /// The kills.
        /// </value>
        public int kills { get; set; }

        /// <summary>
        /// Gets or sets the shots fired with the weapon.
        /// </summary>
        /// <value>
        /// The shots.
        /// </value>
        public int shots { get; set; }

        /// <summary>
        /// Gets or sets the shots that hit with the weapon.
        /// </summary>
        /// <value>
        /// The hits.
        /// </value>
        public int hits { get; set; }

        private decimal _accuracy = 0;

        /// <summary>
        /// Gets the accuracy percentage with the weapon. 0 is returned when no shots were fired.
        /// </summary>
        /// <value>
        /// The accuracy percentage.
        /// </value>
        public decimal accuracy
        {
            get
            {
                if (_accuracy == 0 && shots != 0)
                {
                    _accuracy = (decimal)hits / shots * 100;
                }
                return Math.Round(_accuracy, 2);
            }
        }
    }
}
