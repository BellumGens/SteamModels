using System;
using System.Collections.Generic;
using System.Linq;

namespace SteamModels.Dota2
{
    /// <summary>
    /// View Model aggregating what a single player did across a set of Dota 2 matches.
    /// Dota 2 exposes almost nothing through the generic Steam user stats interface, so a player
    /// profile has to be built from match details. Fetch the match ids with
    /// <see cref="Dota2MatchHistory"/>, fetch each <see cref="Dota2MatchDetails"/> and pass the
    /// results here.
    /// </summary>
    public class Dota2PlayerSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dota2PlayerSummary"/> class.
        /// Matches the player did not play in, and matches that came back as an error, are skipped.
        /// </summary>
        /// <param name="accountId">The 32 bit account id of the player to summarise.</param>
        /// <param name="matches">The match details to summarise, i.e. the result property of each
        /// <see cref="Dota2MatchDetails"/> response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="matches"/> is null.</exception>
        public Dota2PlayerSummary(uint accountId, IEnumerable<Dota2MatchDetailsResult> matches)
        {
            if (matches == null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            this.accountId = accountId;

            Dictionary<int, Dota2HeroPlayCount> heroesPlayed = new Dictionary<int, Dota2HeroPlayCount>();

            foreach (Dota2MatchDetailsResult match in matches)
            {
                if (match == null || !match.success)
                {
                    continue;
                }

                Dota2MatchPlayer player = match.GetPlayer(accountId);
                if (player == null)
                {
                    continue;
                }

                this.matches++;
                bool won = match.DidWin(player);
                if (won)
                {
                    wins++;
                }
                else
                {
                    losses++;
                }

                kills += player.kills;
                deaths += player.deaths;
                assists += player.assists;
                lastHits += player.last_hits;
                denies += player.denies;
                _goldPerMinute += player.gold_per_min;
                _experiencePerMinute += player.xp_per_min;
                _duration += match.duration;

                if (player.abandoned)
                {
                    abandons++;
                }

                if (!heroesPlayed.TryGetValue(player.hero_id, out Dota2HeroPlayCount hero))
                {
                    hero = new Dota2HeroPlayCount(player.hero_id);
                    heroesPlayed.Add(player.hero_id, hero);
                }
                hero.Record(won);
            }

            heroes = heroesPlayed.Values
                                 .OrderByDescending(h => h.matches)
                                 .ThenByDescending(h => h.wins)
                                 .ToList();
        }

        private long _goldPerMinute;
        private long _experiencePerMinute;
        private long _duration;

        /// <summary>
        /// Gets the 32 bit account id of the player.
        /// </summary>
        /// <value>
        /// The account id.
        /// </value>
        public uint accountId { get; }

        /// <summary>
        /// Gets the 64 bit steam id of the player.
        /// </summary>
        /// <value>
        /// The 64 bit steam id of the player.
        /// </value>
        public long steamId64 => Dota2Ids.ToSteamId64(accountId);

        /// <summary>
        /// Gets the number of matches the player was found in.
        /// </summary>
        /// <value>
        /// The number of matches.
        /// </value>
        public int matches { get; }

        /// <summary>
        /// Gets the number of matches the player won.
        /// </summary>
        /// <value>
        /// The wins.
        /// </value>
        public int wins { get; }

        /// <summary>
        /// Gets the number of matches the player lost.
        /// </summary>
        /// <value>
        /// The losses.
        /// </value>
        public int losses { get; }

        /// <summary>
        /// Gets the number of matches the player abandoned.
        /// </summary>
        /// <value>
        /// The abandons.
        /// </value>
        public int abandons { get; }

        /// <summary>
        /// Gets the total kills the player scored.
        /// </summary>
        /// <value>
        /// The kills.
        /// </value>
        public int kills { get; }

        /// <summary>
        /// Gets the total number of times the player died.
        /// </summary>
        /// <value>
        /// The deaths.
        /// </value>
        public int deaths { get; }

        /// <summary>
        /// Gets the total assists the player recorded.
        /// </summary>
        /// <value>
        /// The assists.
        /// </value>
        public int assists { get; }

        /// <summary>
        /// Gets the total last hits the player landed.
        /// </summary>
        /// <value>
        /// The last hits.
        /// </value>
        public int lastHits { get; }

        /// <summary>
        /// Gets the total denies the player landed.
        /// </summary>
        /// <value>
        /// The denies.
        /// </value>
        public int denies { get; }

        /// <summary>
        /// Gets the percentage of matches the player won. 0 is returned when no matches were found.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public decimal winRate => Percentage(wins, matches);

        /// <summary>
        /// Gets the kills of the player per death, counting a death free run as one death.
        /// </summary>
        /// <value>
        /// The kill death ratio.
        /// </value>
        public decimal killDeathRatio => Round((decimal)kills / (deaths == 0 ? 1 : deaths));

        /// <summary>
        /// Gets the kills and assists of the player per death, counting a death free run as one death.
        /// </summary>
        /// <value>
        /// The kda.
        /// </value>
        public decimal kda => Round((decimal)(kills + assists) / (deaths == 0 ? 1 : deaths));

        /// <summary>
        /// Gets the average kills the player scored per match.
        /// </summary>
        /// <value>
        /// The average kills.
        /// </value>
        public decimal averageKills => Average(kills);

        /// <summary>
        /// Gets the average number of times the player died per match.
        /// </summary>
        /// <value>
        /// The average deaths.
        /// </value>
        public decimal averageDeaths => Average(deaths);

        /// <summary>
        /// Gets the average assists the player recorded per match.
        /// </summary>
        /// <value>
        /// The average assists.
        /// </value>
        public decimal averageAssists => Average(assists);

        /// <summary>
        /// Gets the average last hits the player landed per match.
        /// </summary>
        /// <value>
        /// The average last hits.
        /// </value>
        public decimal averageLastHits => Average(lastHits);

        /// <summary>
        /// Gets the average gold per minute the player earned.
        /// </summary>
        /// <value>
        /// The average gold per minute.
        /// </value>
        public decimal averageGoldPerMinute => Average(_goldPerMinute);

        /// <summary>
        /// Gets the average experience per minute the player earned.
        /// </summary>
        /// <value>
        /// The average experience per minute.
        /// </value>
        public decimal averageExperiencePerMinute => Average(_experiencePerMinute);

        /// <summary>
        /// Gets the total time the player spent in the matches.
        /// </summary>
        /// <value>
        /// The total time played.
        /// </value>
        public TimeSpan timePlayed => TimeSpan.FromSeconds(_duration);

        /// <summary>
        /// Gets the heroes the player played, most played first.
        /// </summary>
        /// <value>
        /// The heroes.
        /// </value>
        public List<Dota2HeroPlayCount> heroes { get; }

        /// <summary>
        /// Gets the hero the player played the most.
        /// </summary>
        /// <value>
        /// The favourite hero, or null when no matches were found.
        /// </value>
        public Dota2HeroPlayCount favouriteHero => heroes.FirstOrDefault();

        /// <summary>
        /// Divides <paramref name="total"/> by the number of matches, returning 0 when no matches were found.
        /// </summary>
        private decimal Average(long total)
        {
            return matches == 0 ? 0 : Round((decimal)total / matches);
        }

        /// <summary>
        /// Expresses <paramref name="part"/> as a percentage of <paramref name="whole"/>,
        /// returning 0 when <paramref name="whole"/> is 0.
        /// </summary>
        internal static decimal Percentage(int part, int whole)
        {
            return whole == 0 ? 0 : Round((decimal)part / whole * 100);
        }

        /// <summary>
        /// Rounds a value to two decimal places.
        /// </summary>
        internal static decimal Round(decimal value)
        {
            return Math.Round(value, 2);
        }
    }

    /// <summary>
    /// Describes how often a player picked a single hero and how they did on it.
    /// </summary>
    public class Dota2HeroPlayCount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dota2HeroPlayCount"/> class.
        /// </summary>
        /// <param name="heroId">The id of the hero.</param>
        public Dota2HeroPlayCount(int heroId)
        {
            this.heroId = heroId;
        }

        /// <summary>
        /// Records a match played on the hero.
        /// </summary>
        /// <param name="won">Whether the match was won.</param>
        internal void Record(bool won)
        {
            matches++;
            if (won)
            {
                wins++;
            }
            else
            {
                losses++;
            }
        }

        /// <summary>
        /// Gets the id of the hero. See <see cref="Dota2Hero"/> for the hero list.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int heroId { get; }

        /// <summary>
        /// Gets the number of matches played on the hero.
        /// </summary>
        /// <value>
        /// The number of matches.
        /// </value>
        public int matches { get; private set; }

        /// <summary>
        /// Gets the number of matches won on the hero.
        /// </summary>
        /// <value>
        /// The wins.
        /// </value>
        public int wins { get; private set; }

        /// <summary>
        /// Gets the number of matches lost on the hero.
        /// </summary>
        /// <value>
        /// The losses.
        /// </value>
        public int losses { get; private set; }

        /// <summary>
        /// Gets the percentage of matches won on the hero.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public decimal winRate => Dota2PlayerSummary.Percentage(wins, matches);
    }
}
