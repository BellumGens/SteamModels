using System.Text.Json;
using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2TeamInfoTests
    {
        private const string TeamInfo = """
        {"result":{"status":1,"teams":[{"team_id":1838315,"name":"Team Secret","tag":"Secret",
        "time_created":1408905600,"rating":"inactive","logo":892329808893452930,"logo_sponsor":0,
        "country_code":"eu","url":"teamsecret.gg","games_played":500,
        "player_0_account_id":86745912,"player_1_account_id":70388657,"player_2_account_id":0,
        "player_3_account_id":0,"player_4_account_id":0,"player_5_account_id":0,
        "player_6_account_id":0,"admin_account_id":86745912}]}}
        """;

        [Fact]
        public void Deserializes_the_team_fields()
        {
            Dota2TeamDetails team =
                Assert.Single(JsonSerializer.Deserialize<Dota2TeamInfo>(TeamInfo).result.teams);

            Assert.Equal(1838315u, team.team_id);
            Assert.Equal("Team Secret", team.name);
            Assert.Equal("Secret", team.tag);
            Assert.Equal("eu", team.country_code);
            Assert.Equal(500, team.games_played);
            Assert.Equal(892329808893452930UL, team.logo);
            Assert.Equal(1408905600L, team.timeCreated.ToUnixTimeSeconds());
        }

        [Fact]
        public void A_non_numeric_rating_parses_to_null_rather_than_throwing()
        {
            Dota2TeamDetails team =
                JsonSerializer.Deserialize<Dota2TeamInfo>(TeamInfo).result.teams[0];

            Assert.Equal("inactive", team.rating);
            Assert.Null(team.ratingValue);
        }

        [Fact]
        public void A_numeric_rating_parses()
        {
            Dota2TeamDetails team = JsonSerializer.Deserialize<Dota2TeamInfo>("""
            {"result":{"status":1,"teams":[{"team_id":39,"name":"Evil Geniuses","rating":"1450"}]}}
            """).result.teams[0];

            Assert.Equal(1450, team.ratingValue);
        }

        [Fact]
        public void The_roster_skips_the_slots_the_steam_api_left_empty()
        {
            Dota2TeamDetails team =
                JsonSerializer.Deserialize<Dota2TeamInfo>(TeamInfo).result.teams[0];

            Assert.Equal(new uint[] { 86745912, 70388657 }, team.playerAccountIds);
        }
    }
}
