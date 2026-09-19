using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 top live game interface, which lists the
    /// highest rated public games currently being played. Unlike most Dota 2 interfaces the payload
    /// is returned at the root of the response rather than under a result property.
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetTopLiveGame/v1/?key=<API_KEY>&partner=0
    /// </summary>
    public class Dota2TopLiveGames
    {
        /// <summary>
        /// Gets or sets the games currently being played.
        /// </summary>
        /// <value>
        /// The game list.
        /// </value>
        public List<Dota2TopLiveGame> game_list { get; set; }
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
        /// Gets or sets the steam id of the game server, which the Dota 2 real time stats
        /// interface takes as its server_steam_id parameter.
        /// </summary>
        /// <value>
        /// The server steam id.
        /// </value>
        public ulong server_steam_id { get; set; }

        /// <summary>
        /// Gets or sets the lobby id.
        /// </summary>
        /// <value>
        /// The lobby id.
        /// </value>
        public ulong lobby_id { get; set; }

        /// <summary>
        /// Gets or sets the id of the league the game is played in, 0 for public games.
        /// </summary>
        /// <value>
        /// The league id.
        /// </value>
        public int league_id { get; set; }

        /// <summary>
        /// Gets or sets the lobby the game is played in. See <see cref="lobbyType"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The lobby type.
        /// </value>
        public int lobby_type { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds the game has been running for.
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
        public long sort_score { get; set; }

        /// <summary>
        /// Gets or sets the unix timestamp the entry was last updated at.
        /// </summary>
        /// <value>
        /// The last update time.
        /// </value>
        public long last_update_time { get; set; }

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
        /// Gets or sets the name of the Radiant team, only returned for team games.
        /// </summary>
        /// <value>
        /// The Radiant team name.
        /// </value>
        public string team_name_radiant { get; set; }

        /// <summary>
        /// Gets or sets the name of the Dire team, only returned for team games.
        /// </summary>
        /// <value>
        /// The Dire team name.
        /// </value>
        public string team_name_dire { get; set; }

        /// <summary>
        /// Gets or sets the players in the game.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Dota2TopLiveGamePlayer> players { get; set; }

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
        /// Gets the length of time the game has been running for.
        /// </summary>
        /// <value>
        /// The length of time the game has been running for.
        /// </value>
        [JsonIgnore]
        public TimeSpan gameTime => TimeSpan.FromSeconds(game_time);
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
        /// Gets or sets the id of the hero the player is on.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

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
