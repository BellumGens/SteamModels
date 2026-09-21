using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 top live game interface, which lists the
    /// highest rated public games currently being played. Unlike most Dota 2 interfaces the payload
    /// is returned at the root of the response rather than under a result property.
    /// Several of the 64 bit ids are returned as strings rather than numbers by this interface, so
    /// they are typed as strings here and parsed by the helpers next to them.
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetTopLiveGame/v1/?key=<API_KEY>&partner=0
    /// </summary>
    public class Dota2TopLiveGames
    {
        /// <summary>
        /// Gets or sets the search key the games were selected with.
        /// </summary>
        /// <value>
        /// The search key.
        /// </value>
        public string search_key { get; set; }

        /// <summary>
        /// Gets or sets the league id the games were filtered by, 0 when unfiltered.
        /// </summary>
        /// <value>
        /// The league id.
        /// </value>
        public int league_id { get; set; }

        /// <summary>
        /// Gets or sets the hero id the games were filtered by, 0 when unfiltered.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

        /// <summary>
        /// Gets or sets the index the listing started at.
        /// </summary>
        /// <value>
        /// The start game.
        /// </value>
        public int start_game { get; set; }

        /// <summary>
        /// Gets or sets the number of games requested.
        /// </summary>
        /// <value>
        /// The number of games.
        /// </value>
        public int num_games { get; set; }

        /// <summary>
        /// Gets or sets the index of the listing.
        /// </summary>
        /// <value>
        /// The game list index.
        /// </value>
        public int game_list_index { get; set; }

        /// <summary>
        /// Gets or sets the games currently being played.
        /// </summary>
        /// <value>
        /// The game list.
        /// </value>
        public List<Dota2TopLiveGame> game_list { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether specific games were requested.
        /// </summary>
        /// <value>
        ///   <c>true</c> if specific games were requested; otherwise, <c>false</c>.
        /// </value>
        public bool specific_games { get; set; }

        /// <summary>
        /// Gets or sets the bot game, which the Steam API returns zeroed out when there is none.
        /// </summary>
        /// <value>
        /// The bot game.
        /// </value>
        public Dota2TopLiveGame bot_game { get; set; }
    }

    /// <summary>
    /// Describes a single Dota 2 game in the top live game list.
    /// </summary>
    public class Dota2TopLiveGame
    {
        /// <summary>
        /// Gets or sets the unix timestamp the game was added to the list at.
        /// </summary>
        /// <value>
        /// The activate time.
        /// </value>
        public long activate_time { get; set; }

        /// <summary>
        /// Gets or sets the unix timestamp the game is removed from the list at.
        /// </summary>
        /// <value>
        /// The deactivate time.
        /// </value>
        public long deactivate_time { get; set; }

        /// <summary>
        /// Gets or sets the steam id of the game server, which the Dota 2 real time stats interface
        /// takes as its server_steam_id parameter. Returned as a string by this interface.
        /// See <see cref="serverSteamId"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The server steam id.
        /// </value>
        public string server_steam_id { get; set; }

        /// <summary>
        /// Gets or sets the lobby id. Returned as a string by this interface.
        /// See <see cref="lobbyId"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The lobby id.
        /// </value>
        public string lobby_id { get; set; }

        /// <summary>
        /// Gets or sets the match id. Returned as a string by this interface.
        /// See <see cref="matchId"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The match id.
        /// </value>
        public string match_id { get; set; }

        /// <summary>
        /// Gets or sets the id of the league the game is played in, 0 for public games.
        /// </summary>
        /// <value>
        /// The league id.
        /// </value>
        public int league_id { get; set; }

        /// <summary>
        /// Gets or sets the id of the series the game belongs to, 0 outside of a series.
        /// </summary>
        /// <value>
        /// The series id.
        /// </value>
        public int series_id { get; set; }

        /// <summary>
        /// Gets or sets the lobby the game is played in. See <see cref="lobbyType"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The lobby type.
        /// </value>
        public int lobby_type { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds the game has been running for.
        /// This is negative while the game is still in its pre game phase.
        /// </summary>
        /// <value>
        /// The game time.
        /// </value>
        public int game_time { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds the broadcast is delayed by.
        /// </summary>
        /// <value>
        /// The delay.
        /// </value>
        public int delay { get; set; }

        /// <summary>
        /// Gets or sets the number of people currently spectating the game.
        /// </summary>
        /// <value>
        /// The spectators.
        /// </value>
        public int spectators { get; set; }

        /// <summary>
        /// Gets or sets the game mode. See <see cref="gameMode"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The game mode.
        /// </value>
        public int game_mode { get; set; }

        /// <summary>
        /// Gets or sets the average matchmaking rating of the players in the game.
        /// </summary>
        /// <value>
        /// The average mmr.
        /// </value>
        public int average_mmr { get; set; }

        /// <summary>
        /// Gets or sets the score the list is sorted by.
        /// </summary>
        /// <value>
        /// The sort score.
        /// </value>
        public int sort_score { get; set; }

        /// <summary>
        /// Gets or sets the unix timestamp the entry was last updated at.
        /// The Steam API returns this in scientific notation, which is why it is a double.
        /// See <see cref="lastUpdateTime"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        public double last_update_time { get; set; }

        /// <summary>
        /// Gets or sets the net worth the Radiant is ahead by, negative when the Dire is ahead.
        /// </summary>
        /// <value>
        /// The Radiant lead.
        /// </value>
        public int radiant_lead { get; set; }

        /// <summary>
        /// Gets or sets the number of kills the Radiant has scored.
        /// </summary>
        /// <value>
        /// The Radiant score.
        /// </value>
        public int radiant_score { get; set; }

        /// <summary>
        /// Gets or sets the number of kills the Dire has scored.
        /// </summary>
        /// <value>
        /// The Dire score.
        /// </value>
        public int dire_score { get; set; }

        /// <summary>
        /// Gets or sets the bit mask of the buildings left standing, for both teams.
        /// </summary>
        /// <value>
        /// The building state.
        /// </value>
        public int building_state { get; set; }

        /// <summary>
        /// Gets or sets the id of the Radiant team, 0 outside of team games.
        /// </summary>
        /// <value>
        /// The Radiant team id.
        /// </value>
        public uint team_id_radiant { get; set; }

        /// <summary>
        /// Gets or sets the id of the Dire team, 0 outside of team games.
        /// </summary>
        /// <value>
        /// The Dire team id.
        /// </value>
        public uint team_id_dire { get; set; }

        /// <summary>
        /// Gets or sets the name of the Radiant team, empty outside of team games.
        /// </summary>
        /// <value>
        /// The Radiant team name.
        /// </value>
        public string team_name_radiant { get; set; }

        /// <summary>
        /// Gets or sets the name of the Dire team, empty outside of team games.
        /// </summary>
        /// <value>
        /// The Dire team name.
        /// </value>
        public string team_name_dire { get; set; }

        /// <summary>
        /// Gets or sets the ugc id of the Radiant team logo. Returned as a string by this interface.
        /// </summary>
        /// <value>
        /// The Radiant team logo.
        /// </value>
        public string team_logo_radiant { get; set; }

        /// <summary>
        /// Gets or sets the ugc id of the Dire team logo. Returned as a string by this interface.
        /// </summary>
        /// <value>
        /// The Dire team logo.
        /// </value>
        public string team_logo_dire { get; set; }

        /// <summary>
        /// Gets or sets the id of the weekend tourney the game belongs to, 0 outside of one.
        /// </summary>
        /// <value>
        /// The weekend tourney tournament id.
        /// </value>
        public int weekend_tourney_tournament_id { get; set; }

        /// <summary>
        /// Gets or sets the weekend tourney division.
        /// </summary>
        /// <value>
        /// The weekend tourney division.
        /// </value>
        public int weekend_tourney_division { get; set; }

        /// <summary>
        /// Gets or sets the weekend tourney skill level.
        /// </summary>
        /// <value>
        /// The weekend tourney skill level.
        /// </value>
        public int weekend_tourney_skill_level { get; set; }

        /// <summary>
        /// Gets or sets the weekend tourney bracket round.
        /// </summary>
        /// <value>
        /// The weekend tourney bracket round.
        /// </value>
        public int weekend_tourney_bracket_round { get; set; }

        /// <summary>
        /// Gets or sets the difficulty of the custom game, 0 outside of custom games.
        /// </summary>
        /// <value>
        /// The custom game difficulty.
        /// </value>
        public int custom_game_difficulty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game is a player draft.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the game is a player draft; otherwise, <c>false</c>.
        /// </value>
        public bool is_player_draft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game can be watched.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the game can be watched; otherwise, <c>false</c>.
        /// </value>
        public bool is_watch_eligible { get; set; }

        /// <summary>
        /// Gets or sets the players in the game.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Dota2TopLiveGamePlayer> players { get; set; }

        /// <summary>
        /// Gets the match id, or null when it could not be parsed.
        /// </summary>
        /// <value>
        /// The match id.
        /// </value>
        [JsonIgnore]
        public long? matchId => ParseLong(match_id);

        /// <summary>
        /// Gets the steam id of the game server, or null when it could not be parsed.
        /// </summary>
        /// <value>
        /// The server steam id.
        /// </value>
        [JsonIgnore]
        public ulong? serverSteamId => ParseULong(server_steam_id);

        /// <summary>
        /// Gets the lobby id, or null when it could not be parsed.
        /// </summary>
        /// <value>
        /// The lobby id.
        /// </value>
        [JsonIgnore]
        public ulong? lobbyId => ParseULong(lobby_id);

        /// <summary>
        /// Gets the lobby the game is played in.
        /// </summary>
        /// <value>
        /// The lobby the game is played in.
        /// </value>
        [JsonIgnore]
        public Dota2LobbyType lobbyType => (Dota2LobbyType)lobby_type;

        /// <summary>
        /// Gets the game mode.
        /// </summary>
        /// <value>
        /// The game mode.
        /// </value>
        [JsonIgnore]
        public Dota2GameMode gameMode => (Dota2GameMode)game_mode;

        /// <summary>
        /// Gets the length of time the game has been running for,
        /// which is negative while the game is still in its pre game phase.
        /// </summary>
        /// <value>
        /// The length of time the game has been running for.
        /// </value>
        [JsonIgnore]
        public TimeSpan gameTime => TimeSpan.FromSeconds(game_time);

        /// <summary>
        /// Gets the time the entry was last updated at.
        /// </summary>
        /// <value>
        /// The time the entry was last updated at.
        /// </value>
        [JsonIgnore]
        public DateTimeOffset lastUpdateTime => DateTimeOffset.FromUnixTimeSeconds((long)last_update_time);

        private static long? ParseLong(string value) =>
            long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long parsed)
                ? parsed
                : (long?)null;

        private static ulong? ParseULong(string value) =>
            ulong.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out ulong parsed)
                ? parsed
                : (ulong?)null;
    }

    /// <summary>
    /// Describes a player in the Dota 2 top live game list.
    /// </summary>
    public class Dota2TopLiveGamePlayer
    {
        /// <summary>
        /// Gets or sets the 32 bit account id of the player.
        /// </summary>
        /// <value>
        /// The account id.
        /// </value>
        public uint account_id { get; set; }

        /// <summary>
        /// Gets or sets the id of the hero the player is on, 0 before the hero is picked.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

        /// <summary>
        /// Gets or sets the team the player belongs to, 0 for the Radiant and 1 for the Dire.
        /// See <see cref="playerTeam"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The team.
        /// </value>
        public int team { get; set; }

        /// <summary>
        /// Gets or sets the zero based position of the player within their team.
        /// </summary>
        /// <value>
        /// The team slot.
        /// </value>
        public int team_slot { get; set; }

        /// <summary>
        /// Gets the team the player belongs to.
        /// </summary>
        /// <value>
        /// The team the player belongs to.
        /// </value>
        [JsonIgnore]
        public Dota2Team playerTeam => (Dota2Team)team;

        /// <summary>
        /// Gets the 64 bit steam id of the player.
        /// </summary>
        /// <value>
        /// The 64 bit steam id of the player.
        /// </value>
        [JsonIgnore]
        public long steamId64 => Dota2Ids.ToSteamId64(account_id);
    }
}
