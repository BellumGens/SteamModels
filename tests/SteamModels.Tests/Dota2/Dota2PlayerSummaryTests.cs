using System.Text.Json;
using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2PlayerSummaryTests
    {
        private static Dota2MatchDetailsResult Win() =>
            JsonSerializer.Deserialize<Dota2MatchDetails>(Payloads.MatchDetails).result;

        private static Dota2MatchDetailsResult LossOnAnotherHero() =>
            JsonSerializer.Deserialize<Dota2MatchDetails>(
                Payloads.MatchDetails.Replace("\"radiant_win\":true", "\"radiant_win\":false")
                                     .Replace("\"hero_id\":8,", "\"hero_id\":11,")).result;

        private static Dota2PlayerSummary Summary() =>
            new Dota2PlayerSummary(Payloads.AccountId, new[] { Win(), LossOnAnotherHero(), Win() });

        [Fact]
        public void Counts_wins_and_losses()
        {
            Dota2PlayerSummary summary = Summary();

            Assert.Equal(3, summary.matches);
            Assert.Equal(2, summary.wins);
            Assert.Equal(1, summary.losses);
            Assert.Equal(66.67m, summary.winRate);
        }

        [Fact]
        public void Totals_the_combat_stats()
        {
            Dota2PlayerSummary summary = Summary();

            Assert.Equal(36, summary.kills);
            Assert.Equal(12, summary.deaths);
            Assert.Equal(18, summary.assists);
            Assert.Equal(750, summary.lastHits);
            Assert.Equal(36, summary.denies);
            Assert.Equal(0, summary.abandons);
        }

        [Fact]
        public void Derives_the_ratios_and_averages()
        {
            Dota2PlayerSummary summary = Summary();

            Assert.Equal(3.00m, summary.killDeathRatio);
            Assert.Equal(4.50m, summary.kda);
            Assert.Equal(12m, summary.averageKills);
            Assert.Equal(4m, summary.averageDeaths);
            Assert.Equal(650m, summary.averageGoldPerMinute);
            Assert.Equal(720m, summary.averageExperiencePerMinute);
            Assert.Equal(TimeSpan.FromSeconds(2251 * 3), summary.timePlayed);
            Assert.Equal(Payloads.SteamId64, summary.steamId64);
        }

        [Fact]
        public void Ranks_heroes_by_how_often_they_were_picked()
        {
            Dota2PlayerSummary summary = Summary();

            Assert.Equal(2, summary.heroes.Count);
            Assert.Equal(8, summary.favouriteHero.heroId);
            Assert.Equal(2, summary.favouriteHero.matches);
            Assert.Equal(2, summary.favouriteHero.wins);
            Assert.Equal(0, summary.favouriteHero.losses);
            Assert.Equal(100m, summary.favouriteHero.winRate);

            Dota2HeroPlayCount other = summary.heroes[1];
            Assert.Equal(11, other.heroId);
            Assert.Equal(0m, other.winRate);
        }

        [Fact]
        public void Skips_matches_that_came_back_as_an_error_and_null_entries()
        {
            Dota2MatchDetailsResult error =
                JsonSerializer.Deserialize<Dota2MatchDetails>(Payloads.MatchDetailsError).result;

            Dota2PlayerSummary summary =
                new Dota2PlayerSummary(Payloads.AccountId, new[] { Win(), error, null });

            Assert.Equal(1, summary.matches);
        }

        [Fact]
        public void Skips_matches_the_player_was_not_in()
        {
            Dota2PlayerSummary summary = new Dota2PlayerSummary(12345, new[] { Win() });

            Assert.Equal(0, summary.matches);
        }

        [Fact]
        public void Counts_abandons()
        {
            Dota2PlayerSummary summary =
                new Dota2PlayerSummary(Dota2Ids.AnonymousAccountId, new[] { Win() });

            Assert.Equal(1, summary.matches);
            Assert.Equal(1, summary.abandons);
            Assert.Equal(0, summary.wins);
        }

        [Fact]
        public void An_empty_set_of_matches_divides_by_nothing()
        {
            Dota2PlayerSummary summary =
                new Dota2PlayerSummary(Payloads.AccountId, new List<Dota2MatchDetailsResult>());

            Assert.Equal(0, summary.matches);
            Assert.Equal(0m, summary.winRate);
            Assert.Equal(0m, summary.kda);
            Assert.Equal(0m, summary.killDeathRatio);
            Assert.Equal(0m, summary.averageGoldPerMinute);
            Assert.Equal(TimeSpan.Zero, summary.timePlayed);
            Assert.Null(summary.favouriteHero);
            Assert.Empty(summary.heroes);
        }

        [Fact]
        public void Rejects_a_null_set_of_matches()
        {
            Assert.Throws<ArgumentNullException>(() => new Dota2PlayerSummary(Payloads.AccountId, null));
        }
    }
}
