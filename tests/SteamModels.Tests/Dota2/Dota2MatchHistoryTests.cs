using System.Text.Json;
using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2MatchHistoryTests
    {
        private const string History = """
        {"result":{"status":1,"num_results":1,"total_results":500,"results_remaining":499,
        "matches":[{"match_id":1900000000,"match_seq_num":1700000000,"start_time":1446069903,
        "lobby_type":7,"radiant_team_id":0,"dire_team_id":0,
        "players":[{"account_id":86745912,"player_slot":0,"hero_id":8,
                    "team_number":0,"team_slot":0,"hero_variant":2},
                   {"account_id":4294967295,"player_slot":131,"hero_id":14}]}]}}
        """;

        private const string HiddenHistory = """
        {"result":{"status":15,
        "statusDetail":"Cannot get match history for a user that hasn't allowed it"}}
        """;

        [Fact]
        public void Deserializes_the_paging_fields()
        {
            Dota2MatchHistoryResult result =
                JsonSerializer.Deserialize<Dota2MatchHistory>(History).result;

            Assert.True(result.success);
            Assert.Equal(1, result.num_results);
            Assert.Equal(500, result.total_results);
            Assert.Equal(499, result.results_remaining);
        }

        [Fact]
        public void Deserializes_the_match_summary()
        {
            Dota2MatchSummary match =
                Assert.Single(JsonSerializer.Deserialize<Dota2MatchHistory>(History).result.matches);

            Assert.Equal(1900000000L, match.match_id);
            Assert.Equal(1700000000L, match.match_seq_num);
            Assert.Equal(Dota2LobbyType.Ranked, match.lobbyType);
            Assert.Equal(1446069903L, match.startTime.ToUnixTimeSeconds());
        }

        [Fact]
        public void Parses_the_player_slots()
        {
            Dota2MatchSummary match =
                JsonSerializer.Deserialize<Dota2MatchHistory>(History).result.matches[0];

            Assert.Equal(Dota2Team.Radiant, match.players[0].team);
            Assert.Equal(0, match.players[0].slot);
            Assert.Equal(Payloads.SteamId64, match.players[0].steamId64);
            Assert.Equal(0, match.players[0].team_number);
            Assert.Equal(0, match.players[0].team_slot);
            Assert.Equal(2, match.players[0].hero_variant);

            Assert.Equal(Dota2Team.Dire, match.players[1].team);
            Assert.Equal(3, match.players[1].slot);
            Assert.Null(match.players[1].steamId64);
        }

        [Fact]
        public void A_hidden_history_is_reported_as_a_failure_with_its_detail()
        {
            Dota2MatchHistoryResult result =
                JsonSerializer.Deserialize<Dota2MatchHistory>(HiddenHistory).result;

            Assert.False(result.success);
            Assert.Equal(15, result.status);
            Assert.StartsWith("Cannot get match history", result.statusDetail);
            Assert.Null(result.matches);
        }
    }
}
