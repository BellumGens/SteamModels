using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 match history interface.
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1/?key=<API_KEY>&account_id=<ACCOUNT_ID>&format=json
    /// </summary>
    public class Dota2MatchHistory
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2MatchHistoryResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 match history response.
    /// </summary>
    public class Dota2MatchHistoryResult
    {
        /// <summary>
        /// Gets or sets the status of the request. 1 means success, 15 means the account has
        /// hidden their match data and in both other cases <see cref="statusDetail"/> is populated.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }

        /// <summary>
        /// Gets or sets the description of the status, only returned when the request failed.
        /// </summary>
        /// <value>
        /// The status detail.
        /// </value>
        public string statusDetail { get; set; }

        /// <summary>
        /// Gets or sets the number of matches in this response.
        /// </summary>
        /// <value>
        /// The number of results.
        /// </value>
        public int num_results { get; set; }

        /// <summary>
        /// Gets or sets the total number of matches the query matched.
        /// </summary>
        /// <value>
        /// The total number of results.
        /// </value>
        public int total_results { get; set; }

        /// <summary>
        /// Gets or sets the number of matches left to page through.
        /// </summary>
        /// <value>
        /// The number of results remaining.
        /// </value>
        public int results_remaining { get; set; }

        /// <summary>
        /// Gets or sets the matches.
        /// </summary>
        /// <value>
        /// The matches.
        /// </value>
        public List<Dota2MatchSummary> matches { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request succeeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> when the request succeeded; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool success => status == 1;
    }

    /// <summary>
    /// Describes a single match in a Dota 2 match history response. Only enough detail to
    /// identify the match is returned, call GetMatchDetails with the
    /// <see cref="match_id"/> for the full <see cref="Dota2MatchDetails"/>.
    /// </summary>
    public class Dota2MatchSummary
    {
        /// <summary>
        /// Gets or sets the match id.
        /// </summary>
        /// <value>
        /// The match id.
        /// </value>
        public long match_id { get; set; }

        /// <summary>
        /// Gets or sets the match sequence number, which orders matches by the time they were recorded.
        /// </summary>
        /// <value>
        /// The match sequence number.
        /// </value>
        public long match_seq_num { get; set; }

        /// <summary>
        /// Gets or sets the unix timestamp the match started at.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public long start_time { get; set; }

        /// <summary>
        /// Gets or sets the lobby the match was played in. See <see cref="lobbyType"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The lobby type.
        /// </value>
        public int lobby_type { get; set; }

        /// <summary>
        /// Gets or sets the id of the Radiant team, 0 outside of team matches.
        /// </summary>
        /// <value>
        /// The Radiant team id.
        /// </value>
        public uint radiant_team_id { get; set; }

        /// <summary>
        /// Gets or sets the id of the Dire team, 0 outside of team matches.
        /// </summary>
        /// <value>
        /// The Dire team id.
        /// </value>
        public uint dire_team_id { get; set; }

        /// <summary>
        /// Gets or sets the players in the match.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Dota2MatchHistoryPlayer> players { get; set; }

        /// <summary>
        /// Gets the time the match started at.
        /// </summary>
        /// <value>
        /// The time the match started at.
        /// </value>
        [JsonIgnore]
        public DateTimeOffset startTime => DateTimeOffset.FromUnixTimeSeconds(start_time);

        /// <summary>
        /// Gets the lobby the match was played in.
        /// </summary>
        /// <value>
        /// The lobby the match was played in.
        /// </value>
        [JsonIgnore]
        public Dota2LobbyType lobbyType => (Dota2LobbyType)lobby_type;
    }

    /// <summary>
    /// Describes a player in a Dota 2 match history response.
    /// </summary>
    public class Dota2MatchHistoryPlayer
    {
        /// <summary>
        /// Gets or sets the 32 bit account id of the player.
        /// <see cref="Dota2Ids.AnonymousAccountId"/> is reported for players who have hidden their match history.
        /// </summary>
        /// <value>
        /// The account id.
        /// </value>
        public uint account_id { get; set; }

        /// <summary>
        /// Gets or sets the player slot, which encodes both the team and the position within the team.
        /// See <see cref="team"/> and <see cref="slot"/> for the parsed values.
        /// </summary>
        /// <value>
        /// The player slot.
        /// </value>
        public int player_slot { get; set; }

        /// <summary>
        /// Gets or sets the id of the hero the player picked. See <see cref="Dota2Hero"/> for the hero list.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

        /// <summary>
        /// Gets the team the player played for.
        /// </summary>
        /// <value>
        /// The team the player played for.
        /// </value>
        [JsonIgnore]
        public Dota2Team team => Dota2Ids.GetTeam(player_slot);

        /// <summary>
        /// Gets the zero based position of the player within their team, 0 through 4.
        /// </summary>
        /// <value>
        /// The position of the player within their team.
        /// </value>
        [JsonIgnore]
        public int slot => Dota2Ids.GetSlot(player_slot);

        /// <summary>
        /// Gets the 64 bit steam id of the player, or null when the player is anonymous.
        /// </summary>
        /// <value>
        /// The 64 bit steam id of the player.
        /// </value>
        [JsonIgnore]
        public long? steamId64 => account_id == Dota2Ids.AnonymousAccountId
            ? (long?)null
            : Dota2Ids.ToSteamId64(account_id);
    }
}
