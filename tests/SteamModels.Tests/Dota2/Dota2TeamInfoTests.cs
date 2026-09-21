using System.Text.Json;
using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2TeamInfoTests
    {
        /// <summary>
        /// The shape the Steam API actually returns. It carries no team_id and no rating, so the
        /// caller has to remember the start_at_team_id it asked for.
        /// </summary>
        private const string TeamInfo = """
        {"result":{"status":1,"teams":[{"name":"Team Secret","tag":"Secret","abbreviation":"SEC",
        "time_created":1408993713,"logo":1848171366536285637,"logo_sponsor":540768028333677405,
        "country_code":"ao","url":"","games_played":0,
        "player_0_account_id":5390881,"player_1_account_id":84385735,"player_2_account_id":87278757,
        "player_3_account_id":105599955,"admin_account_id":5390881}]}}
        """;

        [Fact]
        public void Deserializes_the_team_fields()
        {
            Dota2TeamDetails team =
                Assert.Single(JsonSerializer.Deserialize<Dota2TeamInfo>(TeamInfo).result.teams);

            Assert.Equal("Team Secret", team.name);
            Assert.Equal("Secret", team.tag);
            Assert.Equal("SEC", team.abbreviation);
            Assert.Equal("ao", team.country_code);
            Assert.Equal(0, team.games_played);
            Assert.Equal(1848171366536285637UL, team.logo);
            Assert.Equal(540768028333677405UL, team.logo_sponsor);
            Assert.Equal(5390881u, team.admin_account_id);
            Assert.Equal(1408993713L, team.timeCreated.ToUnixTimeSeconds());
        }

        [Fact]
        public void The_roster_skips_the_slots_the_steam_api_left_out()
        {
            Dota2TeamDetails team =
                JsonSerializer.Deserialize<Dota2TeamInfo>(TeamInfo).result.teams[0];

            Assert.Equal(new uint[] { 5390881, 84385735, 87278757, 105599955 }, team.playerAccountIds);
        }

        [Fact]
        public void A_full_roster_reports_all_seven_slots()
        {
            Dota2TeamDetails team = JsonSerializer.Deserialize<Dota2TeamInfo>("""
            {"result":{"status":1,"teams":[{"name":"Full","tag":"F",
            "player_0_account_id":1,"player_1_account_id":2,"player_2_account_id":3,
            "player_3_account_id":4,"player_4_account_id":5,"player_5_account_id":6,
            "player_6_account_id":7}]}}
            """).result.teams[0];

            Assert.Equal(new uint[] { 1, 2, 3, 4, 5, 6, 7 }, team.playerAccountIds);
        }
    }
}
