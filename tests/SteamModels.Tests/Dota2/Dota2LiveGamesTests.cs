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

        /// <summary>
        /// The shape the Steam API actually returns. This interface hands back several of the
        /// 64 bit ids as strings, and last_update_time in scientific notation.
        /// </summary>
        private const string TopLiveGames = """
        {"search_key":"","league_id":0,"hero_id":0,"start_game":0,"num_games":0,"game_list_index":0,
        "game_list":[{"activate_time":1789882577,"deactivate_time":1789882623,
        "server_steam_id":"90293069356269591","lobby_id":"30027431856245751","league_id":0,
        "lobby_type":7,"game_time":-14,"delay":120,"spectators":0,"game_mode":22,"average_mmr":8137,
        "match_id":"9007550858","series_id":0,"team_name_radiant":"","team_name_dire":"",
        "team_logo_radiant":"0","team_logo_dire":"0","team_id_radiant":0,"team_id_dire":0,
        "sort_score":8637,"last_update_time":1.7898825e+09,"radiant_lead":0,
        "radiant_score":0,"dire_score":0,
        "players":[{"account_id":1229557033,"hero_id":0,"team_slot":1,"team":0},
                   {"account_id":202691456,"hero_id":14,"team_slot":0,"team":1}],
        "building_state":19138340,"weekend_tourney_tournament_id":0,"weekend_tourney_division":0,
        "weekend_tourney_skill_level":0,"weekend_tourney_bracket_round":0,
        "custom_game_difficulty":0,"is_player_draft":false,"is_watch_eligible":true}],
        "specific_games":false,
        "bot_game":{"activate_time":0,"deactivate_time":0,"server_steam_id":"0"}}
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

            Assert.Equal(0, top.game_list_index);
            Assert.False(top.specific_games);
            Assert.NotNull(top.bot_game);

            Dota2TopLiveGame game = Assert.Single(top.game_list);
            Assert.Equal(8137, game.average_mmr);
            Assert.Equal(8637, game.sort_score);
            Assert.Equal(19138340, game.building_state);
            Assert.Equal(Dota2GameMode.AllDraft, game.gameMode);
            Assert.Equal(Dota2LobbyType.Ranked, game.lobbyType);
            Assert.True(game.is_watch_eligible);
            Assert.False(game.is_player_draft);
        }

        [Fact]
        public void The_string_ids_this_interface_returns_are_parsed()
        {
            Dota2TopLiveGame game =
                JsonSerializer.Deserialize<Dota2TopLiveGames>(TopLiveGames).game_list[0];

            Assert.Equal("9007550858", game.match_id);
            Assert.Equal(9007550858L, game.matchId);
            Assert.Equal(90293069356269591UL, game.serverSteamId);
            Assert.Equal(30027431856245751UL, game.lobbyId);
        }

        [Fact]
        public void An_unparsable_id_yields_null_rather_than_throwing()
        {
            Dota2TopLiveGame game = new Dota2TopLiveGame { match_id = "", server_steam_id = null };

            Assert.Null(game.matchId);
            Assert.Null(game.serverSteamId);
        }

        [Fact]
        public void A_pre_game_lobby_reports_a_negative_game_time()
        {
            Dota2TopLiveGame game =
                JsonSerializer.Deserialize<Dota2TopLiveGames>(TopLiveGames).game_list[0];

            Assert.Equal(-14, game.game_time);
            Assert.Equal(TimeSpan.FromSeconds(-14), game.gameTime);
        }

        [Fact]
        public void The_scientific_notation_timestamp_is_read_as_a_number()
        {
            Dota2TopLiveGame game =
                JsonSerializer.Deserialize<Dota2TopLiveGames>(TopLiveGames).game_list[0];

            Assert.Equal(1789882500L, game.lastUpdateTime.ToUnixTimeSeconds());
        }

        [Fact]
        public void Top_live_game_players_carry_a_team_and_slot()
        {
            Dota2TopLiveGame game =
                JsonSerializer.Deserialize<Dota2TopLiveGames>(TopLiveGames).game_list[0];

            Assert.Equal(2, game.players.Count);
            Assert.Equal(Dota2Team.Radiant, game.players[0].playerTeam);
            Assert.Equal(1, game.players[0].team_slot);
            Assert.Equal(Dota2Team.Dire, game.players[1].playerTeam);
            Assert.Equal(Dota2Ids.ToSteamId64(1229557033), game.players[0].steamId64);
        }
    }
}
