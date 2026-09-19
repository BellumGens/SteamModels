using System.Text.Json;
using SteamModels.CSGO;

namespace SteamModels.Tests.CSGO
{
    public class CSGOPlayerStatsTests
    {
        private const string PrivateProfile = """{"playerstats":{"error":"Profile is not public"}}""";

        private const string ZeroedStats = """
        {"playerstats":{"steamID":"76561198047011640","gameName":"ValveTestApp260","stats":[
          {"name":"total_kills","value":0},{"name":"total_deaths","value":0},
          {"name":"total_shots_fired","value":0},{"name":"total_shots_hit","value":0},
          {"name":"total_kills_headshot","value":0}]}}
        """;

        private const string RealStats = """
        {"playerstats":{"steamID":"76561198047011640","gameName":"ValveTestApp260","stats":[
          {"name":"total_kills","value":1000},{"name":"total_deaths","value":800},
          {"name":"total_shots_fired","value":10000},{"name":"total_shots_hit","value":2200},
          {"name":"total_kills_headshot","value":450},
          {"name":"total_kills_ak47","value":400},{"name":"total_shots_ak47","value":3000},
          {"name":"total_hits_ak47","value":750},
          {"name":"total_kills_awp","value":200},{"name":"total_shots_awp","value":500},
          {"name":"total_hits_awp","value":250},
          {"name":"total_kills_knife_fight","value":900},
          {"name":"total_kills_enemy_blinded","value":950}]}}
        """;

        private static CSGOPlayerStats Parse(string json) =>
            JsonSerializer.Deserialize<CSGOPlayerStats>(json);

        [Fact]
        public void Derives_the_headline_stats()
        {
            CSGOPlayerStats stats = Parse(RealStats);

            Assert.True(stats.playerstats.success);
            Assert.Equal(1.25m, stats.killDeathRatio);
            Assert.Equal(45m, stats.headshotPercentage);
            Assert.Equal(22m, stats.accuracy);
        }

        [Fact]
        public void Derives_the_per_weapon_breakdown()
        {
            CSGOPlayerStats stats = Parse(RealStats);

            WeaponDescriptor ak = stats.weapons.Single(w => w.name == "ak47");
            Assert.Equal(400, ak.kills);
            Assert.Equal(3000, ak.shots);
            Assert.Equal(750, ak.hits);
            Assert.Equal(25m, ak.accuracy);

            Assert.Equal(50m, stats.weapons.Single(w => w.name == "awp").accuracy);
        }

        [Fact]
        public void The_favourite_weapon_ignores_the_non_weapon_kill_stats()
        {
            CSGOPlayerStats stats = Parse(RealStats);

            // knife_fight and enemy_blinded both outscore the ak47 but are not weapons
            Assert.Equal("ak47", stats.favouriteWeapon.name);
        }

        [Fact]
        public void A_private_profile_surfaces_the_error_instead_of_empty_stats()
        {
            CSGOPlayerStats stats = Parse(PrivateProfile);

            Assert.Equal("Profile is not public", stats.playerstats.error);
            Assert.False(stats.playerstats.success);
            Assert.Null(stats.playerstats.stats);
        }

        [Fact]
        public void A_private_profile_does_not_throw_from_the_derived_stats()
        {
            CSGOPlayerStats stats = Parse(PrivateProfile);

            Assert.Equal(0m, stats.killDeathRatio);
            Assert.Equal(0m, stats.headshotPercentage);
            Assert.Equal(0m, stats.accuracy);
            Assert.Empty(stats.weapons);
            Assert.Null(stats.favouriteWeapon);
        }

        [Fact]
        public void Zeroed_stats_do_not_divide_by_zero()
        {
            CSGOPlayerStats stats = Parse(ZeroedStats);

            Assert.Equal(0m, stats.killDeathRatio);
            Assert.Equal(0m, stats.headshotPercentage);
            Assert.Equal(0m, stats.accuracy);
        }

        [Fact]
        public void A_weapon_with_no_shots_fired_reports_no_accuracy()
        {
            WeaponDescriptor weapon = new WeaponDescriptor { name = "ak47", kills = 0, shots = 0, hits = 0 };

            Assert.Equal(0m, weapon.accuracy);
        }

        [Fact]
        public void Stats_deserialize_into_the_base_model()
        {
            SteamUserStats stats = JsonSerializer.Deserialize<SteamUserStats>(RealStats);

            Assert.Equal("76561198047011640", stats.playerstats.steamID);
            Assert.Equal("ValveTestApp260", stats.playerstats.gameName);
            Assert.Equal(1000, stats.playerstats.stats.Single(s => s.name == "total_kills").value);
        }
    }
}
