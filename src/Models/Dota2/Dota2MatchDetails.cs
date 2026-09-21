using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 match details interface.
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/v1/?key=<API_KEY>&match_id=<MATCH_ID>&format=json
    /// </summary>
    public class Dota2MatchDetails
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2MatchDetailsResult result { get; set; }
    }

    /// <summary>
    /// Class describing the response format of the Dota 2 match history by sequence number interface,
    /// which returns full match details rather than the summaries returned by
    /// <see cref="Dota2MatchHistory"/>.
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v1/?key=<API_KEY>&start_at_match_seq_num=<MATCH_SEQ_NUM>&format=json
    /// </summary>
    public class Dota2MatchHistoryBySequenceNum
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2MatchHistoryBySequenceNumResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 match history by sequence number response.
    /// </summary>
    public class Dota2MatchHistoryBySequenceNumResult
    {
        /// <summary>
        /// Gets or sets the status of the request. 1 means success.
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
        /// Gets or sets the matches, in full detail.
        /// </summary>
        /// <value>
        /// The matches.
        /// </value>
        public List<Dota2MatchDetailsResult> matches { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 match details response.
    /// </summary>
    public class Dota2MatchDetailsResult
    {
        /// <summary>
        /// Gets or sets the players in the match.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Dota2MatchPlayer> players { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Radiant won the match.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Radiant won; otherwise, <c>false</c>.
        /// </value>
        public bool radiant_win { get; set; }

        /// <summary>
        /// Gets or sets the length of the match in seconds. See <see cref="durationTime"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public int duration { get; set; }

        /// <summary>
        /// Gets or sets the length of the pre game phase in seconds.
        /// </summary>
        /// <value>
        /// The pre game duration.
        /// </value>
        public int pre_game_duration { get; set; }

        /// <summary>
        /// Gets or sets the unix timestamp the match started at. See <see cref="startTime"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public long start_time { get; set; }

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
        /// Gets or sets the bit mask of the Radiant towers left standing.
        /// See <see cref="radiantTowerStatus"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The Radiant tower status.
        /// </value>
        public int tower_status_radiant { get; set; }

        /// <summary>
        /// Gets or sets the bit mask of the Dire towers left standing.
        /// See <see cref="direTowerStatus"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The Dire tower status.
        /// </value>
        public int tower_status_dire { get; set; }

        /// <summary>
        /// Gets or sets the bit mask of the Radiant barracks left standing.
        /// See <see cref="radiantBarracksStatus"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The Radiant barracks status.
        /// </value>
        public int barracks_status_radiant { get; set; }

        /// <summary>
        /// Gets or sets the bit mask of the Dire barracks left standing.
        /// See <see cref="direBarracksStatus"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The Dire barracks status.
        /// </value>
        public int barracks_status_dire { get; set; }

        /// <summary>
        /// Gets or sets the server cluster the match was played on, which maps to a region.
        /// </summary>
        /// <value>
        /// The cluster.
        /// </value>
        public int cluster { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds into the match the first blood was drawn at.
        /// </summary>
        /// <value>
        /// The first blood time.
        /// </value>
        public int first_blood_time { get; set; }

        /// <summary>
        /// Gets or sets the lobby the match was played in. See <see cref="lobbyType"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The lobby type.
        /// </value>
        public int lobby_type { get; set; }

        /// <summary>
        /// Gets or sets the number of human players in the match.
        /// </summary>
        /// <value>
        /// The number of human players.
        /// </value>
        public int human_players { get; set; }

        /// <summary>
        /// Gets or sets the id of the league the match was played in, 0 outside of league play.
        /// </summary>
        /// <value>
        /// The league id.
        /// </value>
        public int leagueid { get; set; }

        /// <summary>
        /// Gets or sets the positive votes the match replay received.
        /// </summary>
        /// <value>
        /// The positive votes.
        /// </value>
        public int positive_votes { get; set; }

        /// <summary>
        /// Gets or sets the negative votes the match replay received.
        /// </summary>
        /// <value>
        /// The negative votes.
        /// </value>
        public int negative_votes { get; set; }

        /// <summary>
        /// Gets or sets the game mode the match was played in. See <see cref="gameMode"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The game mode.
        /// </value>
        public int game_mode { get; set; }

        /// <summary>
        /// Gets or sets the match flags.
        /// </summary>
        /// <value>
        /// The flags.
        /// </value>
        public int flags { get; set; }

        /// <summary>
        /// Gets or sets the version of the game engine the match was played on.
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        public int engine { get; set; }

        /// <summary>
        /// Gets or sets the number of kills the Radiant scored.
        /// </summary>
        /// <value>
        /// The Radiant score.
        /// </value>
        public int radiant_score { get; set; }

        /// <summary>
        /// Gets or sets the number of kills the Dire scored.
        /// </summary>
        /// <value>
        /// The Dire score.
        /// </value>
        public int dire_score { get; set; }

        /// <summary>
        /// Gets or sets the picks and bans, only returned for drafted game modes such as Captains Mode.
        /// </summary>
        /// <value>
        /// The picks and bans.
        /// </value>
        public List<Dota2PickBan> picks_bans { get; set; }

        /// <summary>
        /// Gets or sets the tournament id, only returned for league matches.
        /// </summary>
        /// <value>
        /// The tournament id.
        /// </value>
        public int tournament_id { get; set; }

        /// <summary>
        /// Gets or sets the tournament round, only returned for league matches.
        /// </summary>
        /// <value>
        /// The tournament round.
        /// </value>
        public int tournament_round { get; set; }

        /// <summary>
        /// Gets or sets the id of the Radiant team, only returned for team matches.
        /// </summary>
        /// <value>
        /// The Radiant team id.
        /// </value>
        public uint radiant_team_id { get; set; }

        /// <summary>
        /// Gets or sets the name of the Radiant team, only returned for team matches.
        /// </summary>
        /// <value>
        /// The Radiant team name.
        /// </value>
        public string radiant_name { get; set; }

        /// <summary>
        /// Gets or sets the ugc id of the Radiant team logo, only returned for team matches.
        /// </summary>
        /// <value>
        /// The Radiant team logo.
        /// </value>
        public ulong radiant_logo { get; set; }

        /// <summary>
        /// Gets or sets whether the Radiant team was complete, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the Radiant team was complete.
        /// </value>
        public int radiant_team_complete { get; set; }

        /// <summary>
        /// Gets or sets the id of the Dire team, only returned for team matches.
        /// </summary>
        /// <value>
        /// The Dire team id.
        /// </value>
        public uint dire_team_id { get; set; }

        /// <summary>
        /// Gets or sets the name of the Dire team, only returned for team matches.
        /// </summary>
        /// <value>
        /// The Dire team name.
        /// </value>
        public string dire_name { get; set; }

        /// <summary>
        /// Gets or sets the ugc id of the Dire team logo, only returned for team matches.
        /// </summary>
        /// <value>
        /// The Dire team logo.
        /// </value>
        public ulong dire_logo { get; set; }

        /// <summary>
        /// Gets or sets whether the Dire team was complete, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the Dire team was complete.
        /// </value>
        public int dire_team_complete { get; set; }

        /// <summary>
        /// Gets or sets the account id of the Radiant captain, only returned for drafted game modes.
        /// </summary>
        /// <value>
        /// The Radiant captain.
        /// </value>
        public uint radiant_captain { get; set; }

        /// <summary>
        /// Gets or sets the account id of the Dire captain, only returned for drafted game modes.
        /// </summary>
        /// <value>
        /// The Dire captain.
        /// </value>
        public uint dire_captain { get; set; }

        /// <summary>
        /// Gets or sets the error returned instead of the match, e.g. "Match ID not found".
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string error { get; set; }

        /// <summary>
        /// Gets a value indicating whether the match details were returned rather than an error.
        /// </summary>
        /// <value>
        ///   <c>true</c> when the match details were returned; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool success => error == null && players != null;

        /// <summary>
        /// Gets the time the match started at.
        /// </summary>
        /// <value>
        /// The time the match started at.
        /// </value>
        [JsonIgnore]
        public DateTimeOffset startTime => DateTimeOffset.FromUnixTimeSeconds(start_time);

        /// <summary>
        /// Gets the length of the match.
        /// </summary>
        /// <value>
        /// The length of the match.
        /// </value>
        [JsonIgnore]
        public TimeSpan durationTime => TimeSpan.FromSeconds(duration);

        /// <summary>
        /// Gets the game mode the match was played in.
        /// </summary>
        /// <value>
        /// The game mode.
        /// </value>
        [JsonIgnore]
        public Dota2GameMode gameMode => (Dota2GameMode)game_mode;

        /// <summary>
        /// Gets the lobby the match was played in.
        /// </summary>
        /// <value>
        /// The lobby the match was played in.
        /// </value>
        [JsonIgnore]
        public Dota2LobbyType lobbyType => (Dota2LobbyType)lobby_type;

        /// <summary>
        /// Gets the team that won the match.
        /// </summary>
        /// <value>
        /// The team that won the match.
        /// </value>
        [JsonIgnore]
        public Dota2Team winner => radiant_win ? Dota2Team.Radiant : Dota2Team.Dire;

        /// <summary>
        /// Gets the Radiant towers left standing at the end of the match.
        /// </summary>
        /// <value>
        /// The Radiant towers left standing.
        /// </value>
        [JsonIgnore]
        public Dota2TowerStatus radiantTowerStatus => (Dota2TowerStatus)tower_status_radiant;

        /// <summary>
        /// Gets the Dire towers left standing at the end of the match.
        /// </summary>
        /// <value>
        /// The Dire towers left standing.
        /// </value>
        [JsonIgnore]
        public Dota2TowerStatus direTowerStatus => (Dota2TowerStatus)tower_status_dire;

        /// <summary>
        /// Gets the Radiant barracks left standing at the end of the match.
        /// </summary>
        /// <value>
        /// The Radiant barracks left standing.
        /// </value>
        [JsonIgnore]
        public Dota2BarracksStatus radiantBarracksStatus => (Dota2BarracksStatus)barracks_status_radiant;

        /// <summary>
        /// Gets the Dire barracks left standing at the end of the match.
        /// </summary>
        /// <value>
        /// The Dire barracks left standing.
        /// </value>
        [JsonIgnore]
        public Dota2BarracksStatus direBarracksStatus => (Dota2BarracksStatus)barracks_status_dire;

        /// <summary>
        /// Gets the player with the given account id.
        /// </summary>
        /// <param name="accountId">The 32 bit account id of the player.</param>
        /// <returns>The player, or null when they did not play in this match.</returns>
        public Dota2MatchPlayer GetPlayer(uint accountId)
        {
            return players?.FirstOrDefault(p => p.account_id == accountId);
        }

        /// <summary>
        /// Gets a value indicating whether the given player won the match.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns><c>true</c> when the player won the match; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="player"/> is null.</exception>
        public bool DidWin(Dota2MatchPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }
            return player.team == winner;
        }
    }

    /// <summary>
    /// Describes a player in a Dota 2 match details response.
    /// </summary>
    public class Dota2MatchPlayer
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
        /// Gets or sets the team number, 0 for the Radiant and 1 for the Dire.
        /// </summary>
        /// <value>
        /// The team number.
        /// </value>
        public int team_number { get; set; }

        /// <summary>
        /// Gets or sets the zero based position of the player within their team.
        /// </summary>
        /// <value>
        /// The team slot.
        /// </value>
        public int team_slot { get; set; }

        /// <summary>
        /// Gets or sets the id of the hero the player picked. See <see cref="Dota2Hero"/> for the hero list.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

        /// <summary>
        /// Gets or sets the facet, or hero variant, the player picked.
        /// </summary>
        /// <value>
        /// The hero variant.
        /// </value>
        public int hero_variant { get; set; }

        /// <summary>
        /// Gets or sets the persona name of the player, only returned for professional players.
        /// </summary>
        /// <value>
        /// The persona.
        /// </value>
        public string persona { get; set; }

        /// <summary>
        /// Gets or sets the item in the first inventory slot.
        /// The Steam API no longer exposes an item list to resolve the id against, so it has to
        /// be looked up against the game files or a third party item list.
        /// </summary>
        /// <value>
        /// The item in the first inventory slot.
        /// </value>
        public int item_0 { get; set; }

        /// <summary>
        /// Gets or sets the item in the second inventory slot.
        /// </summary>
        /// <value>
        /// The item in the second inventory slot.
        /// </value>
        public int item_1 { get; set; }

        /// <summary>
        /// Gets or sets the item in the third inventory slot.
        /// </summary>
        /// <value>
        /// The item in the third inventory slot.
        /// </value>
        public int item_2 { get; set; }

        /// <summary>
        /// Gets or sets the item in the fourth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the fourth inventory slot.
        /// </value>
        public int item_3 { get; set; }

        /// <summary>
        /// Gets or sets the item in the fifth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the fifth inventory slot.
        /// </value>
        public int item_4 { get; set; }

        /// <summary>
        /// Gets or sets the item in the sixth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the sixth inventory slot.
        /// </value>
        public int item_5 { get; set; }

        /// <summary>
        /// Gets or sets the item in the first backpack slot.
        /// </summary>
        /// <value>
        /// The item in the first backpack slot.
        /// </value>
        public int backpack_0 { get; set; }

        /// <summary>
        /// Gets or sets the item in the second backpack slot.
        /// </summary>
        /// <value>
        /// The item in the second backpack slot.
        /// </value>
        public int backpack_1 { get; set; }

        /// <summary>
        /// Gets or sets the item in the third backpack slot.
        /// </summary>
        /// <value>
        /// The item in the third backpack slot.
        /// </value>
        public int backpack_2 { get; set; }

        /// <summary>
        /// Gets or sets the item in the neutral item slot.
        /// </summary>
        /// <value>
        /// The neutral item.
        /// </value>
        public int item_neutral { get; set; }

        /// <summary>
        /// Gets or sets the item in the second neutral slot, i.e. the neutral item enchantment.
        /// </summary>
        /// <value>
        /// The second neutral item.
        /// </value>
        public int item_neutral2 { get; set; }

        /// <summary>
        /// Gets or sets the kills the player scored.
        /// </summary>
        /// <value>
        /// The kills.
        /// </value>
        public int kills { get; set; }

        /// <summary>
        /// Gets or sets the number of times the player died.
        /// </summary>
        /// <value>
        /// The deaths.
        /// </value>
        public int deaths { get; set; }

        /// <summary>
        /// Gets or sets the assists the player recorded.
        /// </summary>
        /// <value>
        /// The assists.
        /// </value>
        public int assists { get; set; }

        /// <summary>
        /// Gets or sets whether and how the player left the match.
        /// See <see cref="leaverStatus"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The leaver status.
        /// </value>
        public int leaver_status { get; set; }

        /// <summary>
        /// Gets or sets the last hits the player landed.
        /// </summary>
        /// <value>
        /// The last hits.
        /// </value>
        public int last_hits { get; set; }

        /// <summary>
        /// Gets or sets the denies the player landed.
        /// </summary>
        /// <value>
        /// The denies.
        /// </value>
        public int denies { get; set; }

        /// <summary>
        /// Gets or sets the average gold per minute the player earned.
        /// </summary>
        /// <value>
        /// The gold per minute.
        /// </value>
        public int gold_per_min { get; set; }

        /// <summary>
        /// Gets or sets the average experience per minute the player earned.
        /// </summary>
        /// <value>
        /// The experience per minute.
        /// </value>
        public int xp_per_min { get; set; }

        /// <summary>
        /// Gets or sets the level the player reached.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int level { get; set; }

        /// <summary>
        /// Gets or sets the net worth of the player at the end of the match.
        /// </summary>
        /// <value>
        /// The net worth.
        /// </value>
        public int net_worth { get; set; }

        /// <summary>
        /// Gets or sets whether the player had an Aghanim's Scepter buff, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the player had an Aghanim's Scepter buff.
        /// </value>
        public int aghanims_scepter { get; set; }

        /// <summary>
        /// Gets or sets whether the player had an Aghanim's Shard buff, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the player had an Aghanim's Shard buff.
        /// </value>
        public int aghanims_shard { get; set; }

        /// <summary>
        /// Gets or sets whether the player had a Moon Shard buff, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the player had a Moon Shard buff.
        /// </value>
        public int moonshard { get; set; }

        /// <summary>
        /// Gets or sets the damage the player dealt to enemy heroes.
        /// </summary>
        /// <value>
        /// The hero damage.
        /// </value>
        public int hero_damage { get; set; }

        /// <summary>
        /// Gets or sets the damage the player dealt to enemy buildings.
        /// </summary>
        /// <value>
        /// The tower damage.
        /// </value>
        public int tower_damage { get; set; }

        /// <summary>
        /// Gets or sets the healing the player provided to allied heroes.
        /// </summary>
        /// <value>
        /// The hero healing.
        /// </value>
        public int hero_healing { get; set; }

        /// <summary>
        /// Gets or sets the gold the player held at the end of the match.
        /// </summary>
        /// <value>
        /// The gold.
        /// </value>
        public int gold { get; set; }

        /// <summary>
        /// Gets or sets the gold the player spent over the match.
        /// </summary>
        /// <value>
        /// The gold spent.
        /// </value>
        public int gold_spent { get; set; }

        /// <summary>
        /// Gets or sets the hero damage scaled against the rest of the match.
        /// </summary>
        /// <value>
        /// The scaled hero damage.
        /// </value>
        public int scaled_hero_damage { get; set; }

        /// <summary>
        /// Gets or sets the tower damage scaled against the rest of the match.
        /// </summary>
        /// <value>
        /// The scaled tower damage.
        /// </value>
        public int scaled_tower_damage { get; set; }

        /// <summary>
        /// Gets or sets the hero healing scaled against the rest of the match.
        /// </summary>
        /// <value>
        /// The scaled hero healing.
        /// </value>
        public int scaled_hero_healing { get; set; }

        /// <summary>
        /// Gets or sets the abilities the player levelled, in the order they were levelled.
        /// </summary>
        /// <value>
        /// The ability upgrades.
        /// </value>
        public List<Dota2AbilityUpgrade> ability_upgrades { get; set; }

        /// <summary>
        /// Gets or sets the units the player controlled besides their hero, such as Lone Druid's bear.
        /// </summary>
        /// <value>
        /// The additional units.
        /// </value>
        public List<Dota2AdditionalUnit> additional_units { get; set; }

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

        /// <summary>
        /// Gets whether and how the player left the match.
        /// </summary>
        /// <value>
        /// The leaver status.
        /// </value>
        [JsonIgnore]
        public Dota2LeaverStatus leaverStatus => (Dota2LeaverStatus)leaver_status;

        /// <summary>
        /// Gets a value indicating whether the player abandoned the match.
        /// </summary>
        /// <value>
        ///   <c>true</c> when the player abandoned the match; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool abandoned => leaverStatus != Dota2LeaverStatus.None &&
                                 leaverStatus != Dota2LeaverStatus.Disconnected;

        /// <summary>
        /// Gets the six inventory slots, in order. A slot holding no item reads 0.
        /// </summary>
        /// <value>
        /// The inventory slots.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<int> items => new[] { item_0, item_1, item_2, item_3, item_4, item_5 };

        /// <summary>
        /// Gets the three backpack slots, in order. A slot holding no item reads 0.
        /// </summary>
        /// <value>
        /// The backpack slots.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<int> backpack => new[] { backpack_0, backpack_1, backpack_2 };

        /// <summary>
        /// Gets the kills and assists of the player per death, counting a death free match as one death.
        /// </summary>
        /// <value>
        /// The kda.
        /// </value>
        [JsonIgnore]
        public decimal kda => Math.Round((decimal)(kills + assists) / (deaths == 0 ? 1 : deaths), 2);
    }

    /// <summary>
    /// Describes a single ability levelled by a player over the course of a match.
    /// </summary>
    public class Dota2AbilityUpgrade
    {
        /// <summary>
        /// Gets or sets the id of the ability that was levelled.
        /// </summary>
        /// <value>
        /// The ability.
        /// </value>
        public int ability { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds into the match the ability was levelled at.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public int time { get; set; }

        /// <summary>
        /// Gets or sets the hero level the ability was levelled at.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int level { get; set; }
    }

    /// <summary>
    /// Describes a unit a player controlled besides their hero, along with its inventory.
    /// </summary>
    public class Dota2AdditionalUnit
    {
        /// <summary>
        /// Gets or sets the internal name of the unit, e.g. "npc_dota_lone_druid_bear".
        /// </summary>
        /// <value>
        /// The name of the unit.
        /// </value>
        public string unitname { get; set; }

        /// <summary>
        /// Gets or sets the item in the first inventory slot.
        /// </summary>
        /// <value>
        /// The item in the first inventory slot.
        /// </value>
        public int item_0 { get; set; }

        /// <summary>
        /// Gets or sets the item in the second inventory slot.
        /// </summary>
        /// <value>
        /// The item in the second inventory slot.
        /// </value>
        public int item_1 { get; set; }

        /// <summary>
        /// Gets or sets the item in the third inventory slot.
        /// </summary>
        /// <value>
        /// The item in the third inventory slot.
        /// </value>
        public int item_2 { get; set; }

        /// <summary>
        /// Gets or sets the item in the fourth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the fourth inventory slot.
        /// </value>
        public int item_3 { get; set; }

        /// <summary>
        /// Gets or sets the item in the fifth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the fifth inventory slot.
        /// </value>
        public int item_4 { get; set; }

        /// <summary>
        /// Gets or sets the item in the sixth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the sixth inventory slot.
        /// </value>
        public int item_5 { get; set; }

        /// <summary>
        /// Gets or sets the item in the first backpack slot.
        /// </summary>
        /// <value>
        /// The item in the first backpack slot.
        /// </value>
        public int backpack_0 { get; set; }

        /// <summary>
        /// Gets or sets the item in the second backpack slot.
        /// </summary>
        /// <value>
        /// The item in the second backpack slot.
        /// </value>
        public int backpack_1 { get; set; }

        /// <summary>
        /// Gets or sets the item in the third backpack slot.
        /// </summary>
        /// <value>
        /// The item in the third backpack slot.
        /// </value>
        public int backpack_2 { get; set; }

        /// <summary>
        /// Gets or sets the item in the neutral item slot.
        /// </summary>
        /// <value>
        /// The neutral item.
        /// </value>
        public int item_neutral { get; set; }

        /// <summary>
        /// Gets the six inventory slots, in order. A slot holding no item reads 0.
        /// </summary>
        /// <value>
        /// The inventory slots.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<int> items => new[] { item_0, item_1, item_2, item_3, item_4, item_5 };

        /// <summary>
        /// Gets the three backpack slots, in order. A slot holding no item reads 0.
        /// </summary>
        /// <value>
        /// The backpack slots.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<int> backpack => new[] { backpack_0, backpack_1, backpack_2 };
    }

    /// <summary>
    /// Describes a single pick or ban in a drafted match.
    /// </summary>
    public class Dota2PickBan
    {
        /// <summary>
        /// Gets or sets a value indicating whether the hero was picked rather than banned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the hero was picked; otherwise, <c>false</c>.
        /// </value>
        public bool is_pick { get; set; }

        /// <summary>
        /// Gets or sets the id of the hero that was picked or banned.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

        /// <summary>
        /// Gets or sets the team that picked or banned the hero, 0 for the Radiant and 1 for the Dire.
        /// See <see cref="pickingTeam"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The team.
        /// </value>
        public int team { get; set; }

        /// <summary>
        /// Gets or sets the position of the pick or ban within the draft.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int order { get; set; }

        /// <summary>
        /// Gets the team that picked or banned the hero.
        /// </summary>
        /// <value>
        /// The team that picked or banned the hero.
        /// </value>
        [JsonIgnore]
        public Dota2Team pickingTeam => (Dota2Team)team;
    }
}
