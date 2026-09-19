using System.Text.Json;
using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2LiveGamesTests
    {
        private const string LiveLeagueGames = """
        {"result":{"games":[{
          "players":[{"account_id":86745912,"name":"Puppey","hero_id":8,"team":0},
                     {"account_id":70388657,"name":"caster","hero_id":0,"team":2}],
          "radiant_team":{"team_name":"Team Secret","team_id":1838315,
                          "team_logo":892329808893452930,"complete":true},
          "dire_team":{"team_name":"Tundra Esports","team_id":8291895,
                       "team_logo":892329808893452931,"complete":false},
          "lobby_id":26489891350929,"match_id":1900000001,"spectators":45000,
          "league_id":16935,"league_node_id":12,"stream_delay_s":300,
          "radiant_series_wins":1,"dire_series_wins":0,"series_type":1,
          "scoreboard":{"duration":1200.5,"roshan_respawn_timer":180,
            "radiant":{"score":14,"tower_state":2047,"barracks_state":63,
              "players":[{"player_slot":0,"account_id":86745912,"hero_id":8,"kills":5,"death":1,
                          "assists":3,"last_hits":120,"denies":8,"gold":2400,"level":16,
                          "gold_per_min":620,"xp_per_min":700,"net_worth":14000,
                          "ultimate_state":2,"ultimate_cooldown":0,
                          "item0":108,"item1":116,"item2":0,"item3":0,"item4":0,"item5":0,
                          "respawn_timer":0,"position_x":-6606.0,"position_y":-6152.0}],
              "abilities":[{"ability_id":5003,"ability_level":4}],
              "picks":[{"hero_id":8}],"bans":[{"hero_id":41}]},
            "dire":{"score":9,"tower_state":1974,"barracks_state":51,
              "players":[{"player_slot":128,"account_id":70388657,"hero_id":14,"kills":2,"death":4,
                          "assists":1,"last_hits":80,"denies":3,"gold":900,"level":13,
                          "gold_per_min":380,"xp_per_min":420,"net_worth":8000,
                          "ultimate_state":0,"ultimate_cooldown":42,
                          "item0":0,"item1":0,"item2":0,"item3":0,"item4":0,"item5":0,
                          "respawn_timer":12,"position_x":100.0,"position_y":200.0}],
              "picks":[{"hero_id":14}],"bans":[{"hero_id":74}]}}}],"status":200}}
        """;

        private const string TopLiveGames = """
        {"game_list":[{"activate_time":1700000000,"deactivate_time":1700003600,
        "server_steam_id":90142294633437700,"lobby_id":26489891350930,"league_id":0,"lobby_type":7,
        "game_time":900,"delay":120,"spectators":12,"game_mode":22,"average_mmr":9000,
        "sort_score":99,"last_update_time":1700000900,"radiant_lead":4500,
        "radiant_score":12,"dire_score":7,"building_state":16711679,
        "team_id_radiant":0,"team_id_dire":0,
        "players":[{"account_id":86745912,"hero_id":8},{"account_id":70388657,"hero_id":14}]}]}
        """;

        private static Dota2LiveLeagueGame Game =>
            JsonSerializer.Deserialize<Dota2LiveLeagueGames>(LiveLeagueGames).result.games[0];

        [Fact]
        public void Deserializes_the_game_fields()
        {
            Dota2LiveLeagueGame game = Game;

            Assert.Equal(1900000001L, game.match_id);
            Assert.Equal(26489891350929UL, game.lobby_id);
            Assert.Equal(45000, game.spectators);
            Assert.Equal(16935, game.league_id);
            Assert.Equal(300, game.stream_delay_s);
            Assert.Equal(Dota2SeriesType.BestOfThree, game.seriesType);
            Assert.Equal(1, game.radiant_series_wins);
        }

        [Fact]
        public void Deserializes_the_teams()
        {
            Dota2LiveLeagueGame game = Game;

            Assert.Equal("Team Secret", game.radiant_team.team_name);
            Assert.Equal(1838315u, game.radiant_team.team_id);
            Assert.True(game.radiant_team.complete);
            Assert.Equal("Tundra Esports", game.dire_team.team_name);
            Assert.False(game.dire_team.complete);
        }

        [Fact]
        public void Broadcasters_are_told_apart_from_players()
        {
            Dota2LiveLeagueGame game = Game;

            Assert.False(game.players[0].isBroadcaster);
            Assert.Equal("Puppey", game.players[0].name);
            Assert.Equal(Payloads.SteamId64, game.players[0].steamId64);
            Assert.True(game.players[1].isBroadcaster);
        }

        [Fact]
        public void Deserializes_the_scoreboard()
        {
            Dota2Scoreboard scoreboard = Game.scoreboard;

            Assert.Equal(TimeSpan.FromSeconds(1200.5), scoreboard.durationTime);
            Assert.Equal(180f, scoreboard.roshan_respawn_timer);
            Assert.Equal(14, scoreboard.radiant.score);
            Assert.Equal(9, scoreboard.dire.score);
        }

        [Fact]
        public void The_live_scoreboard_uses_death_rather_than_deaths()
        {
            Dota2ScoreboardPlayer player = Game.scoreboard.radiant.players[0];

            Assert.Equal(5, player.kills);
            Assert.Equal(1, player.death);
            Assert.Equal(3, player.assists);
        }

        [Fact]
        public void The_live_scoreboard_uses_unprefixed_item_slots()
        {
            Dota2ScoreboardPlayer player = Game.scoreboard.radiant.players[0];

            Assert.Equal(108, player.item0);
            Assert.Equal(116, player.item1);
            Assert.Equal(new[] { 108, 116, 0, 0, 0, 0 }, player.items);
        }

        [Fact]
        public void A_respawn_timer_means_the_player_is_dead()
        {
            Dota2Scoreboard scoreboard = Game.scoreboard;

            Assert.False(scoreboard.radiant.players[0].isDead);
            Assert.True(scoreboard.dire.players[0].isDead);
            Assert.Equal(12, scoreboard.dire.players[0].respawn_timer);
        }

        [Fact]
        public void Deserializes_the_remaining_scoreboard_player_fields()
        {
            Dota2ScoreboardPlayer player = Game.scoreboard.radiant.players[0];

            Assert.Equal(620, player.gold_per_min);
            Assert.Equal(700, player.xp_per_min);
            Assert.Equal(14000, player.net_worth);
            Assert.Equal(2, player.ultimate_state);
            Assert.Equal(-6606.0f, player.position_x);
            Assert.Equal(-6152.0f, player.position_y);
            Assert.Equal(Payloads.SteamId64, player.steamId64);
        }

        [Fact]
        public void Scoreboard_building_state_decodes_into_flags()
        {
            Dota2Scoreboard scoreboard = Game.scoreboard;

            Assert.Equal((Dota2TowerStatus)2047, scoreboard.radiant.towerStatus);
            Assert.False(scoreboard.dire.towerStatus.HasFlag(Dota2TowerStatus.MiddleTier1));
            Assert.False(scoreboard.dire.barracksStatus.HasFlag(Dota2BarracksStatus.MiddleMelee));
        }

        [Fact]
        public void Deserializes_picks_bans_and_abilities()
        {
            Dota2Scoreboard scoreboard = Game.scoreboard;

            Assert.Equal(8, Assert.Single(scoreboard.radiant.picks).hero_id);
            Assert.Equal(41, Assert.Single(scoreboard.radiant.bans).hero_id);
            Assert.Equal(74, Assert.Single(scoreboard.dire.bans).hero_id);

            Dota2ScoreboardAbility ability = Assert.Single(scoreboard.radiant.abilities);
            Assert.Equal(5003, ability.ability_id);
            Assert.Equal(4, ability.ability_level);

            // abilities are only returned for Ability Draft
            Assert.Null(scoreboard.dire.abilities);
        }

        [Fact]
        public void GetTopLiveGame_returns_its_payload_at_the_root()
        {
            Dota2TopLiveGames top = JsonSerializer.Deserialize<Dota2TopLiveGames>(TopLiveGames);

            Dota2TopLiveGame game = Assert.Single(top.game_list);
            Assert.Equal(90142294633437700UL, game.server_steam_id);
            Assert.Equal(9000, game.average_mmr);
            Assert.Equal(4500, game.radiant_lead);
            Assert.Equal(Dota2GameMode.AllDraft, game.gameMode);
            Assert.Equal(Dota2LobbyType.Ranked, game.lobbyType);
            Assert.Equal(TimeSpan.FromSeconds(900), game.gameTime);
            Assert.Equal(2, game.players.Count);
            Assert.Equal(Payloads.SteamId64, game.players[0].steamId64);
        }
    }
}
