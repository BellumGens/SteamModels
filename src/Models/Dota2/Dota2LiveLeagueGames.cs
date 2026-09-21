using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 live league game interface.
    /// Valve does not publish a schema for the scoreboard payload and its field names do not
    /// follow the ones used by <see cref="Dota2MatchDetails"/>, e.g. the live scoreboard reports
    /// "death" rather than "deaths" and "item0" rather than "item_0".
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetLiveLeagueGames/v1/?key=<API_KEY>&format=json
    /// </summary>
    public class Dota2LiveLeagueGames
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2LiveLeagueGamesResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 live league game response.
    /// </summary>
    public class Dota2LiveLeagueGamesResult
    {
        /// <summary>
        /// Gets or sets the games currently being played.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public List<Dota2LiveLeagueGame> games { get; set; }

        /// <summary>
        /// Gets or sets the status of the request. 200 means success.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }
    }

    /// <summary>
    /// Describes a single Dota 2 league game that is currently being played.
    /// </summary>
    public class Dota2LiveLeagueGame
    {
        /// <summary>
        /// Gets or sets the players and broadcasters in the game.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Dota2LiveLeaguePlayer> players { get; set; }

        /// <summary>
        /// Gets or sets the Radiant team.
        /// </summary>
        /// <value>
        /// The Radiant team.
        /// </value>
        public Dota2LiveLeagueTeam radiant_team { get; set; }

        /// <summary>
        /// Gets or sets the Dire team.
        /// </summary>
        /// <value>
        /// The Dire team.
        /// </value>
        public Dota2LiveLeagueTeam dire_team { get; set; }

        /// <summary>
        /// Gets or sets the lobby id.
        /// </summary>
        /// <value>
        /// The lobby id.
        /// </value>
        public ulong lobby_id { get; set; }

        /// <summary>
        /// Gets or sets the match id, which can be passed to GetMatchDetails once the game ends.
        /// </summary>
        /// <value>
        /// The match id.
        /// </value>
        public long match_id { get; set; }

        /// <summary>
        /// Gets or sets the number of people currently spectating the game.
        /// </summary>
        /// <value>
        /// The spectators.
        /// </value>
        public int spectators { get; set; }

        /// <summary>
        /// Gets or sets the id of the league the game is played in.
        /// </summary>
        /// <value>
        /// The league id.
        /// </value>
        public int league_id { get; set; }

        /// <summary>
        /// Gets or sets the id of the league node, i.e. the position in the tournament bracket.
        /// </summary>
        /// <value>
        /// The league node id.
        /// </value>
        public int league_node_id { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds the broadcast is delayed by.
        /// </summary>
        /// <value>
        /// The stream delay.
        /// </value>
        public int stream_delay_s { get; set; }

        /// <summary>
        /// Gets or sets the number of games the Radiant team has won in the current series.
        /// </summary>
        /// <value>
        /// The Radiant series wins.
        /// </value>
        public int radiant_series_wins { get; set; }

        /// <summary>
        /// Gets or sets the number of games the Dire team has won in the current series.
        /// </summary>
        /// <value>
        /// The Dire series wins.
        /// </value>
        public int dire_series_wins { get; set; }

        /// <summary>
        /// Gets or sets the format of the series. See <see cref="seriesType"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The series type.
        /// </value>
        public int series_type { get; set; }

        /// <summary>
        /// Gets or sets the scoreboard, which is only returned once the game has started.
        /// </summary>
        /// <value>
        /// The scoreboard.
        /// </value>
        public Dota2Scoreboard scoreboard { get; set; }

        /// <summary>
        /// Gets the format of the series.
        /// </summary>
        /// <value>
        /// The format of the series.
        /// </value>
        [JsonIgnore]
        public Dota2SeriesType seriesType => (Dota2SeriesType)series_type;
    }

    /// <summary>
    /// Describes a player or broadcaster in a Dota 2 live league game.
    /// </summary>
    public class Dota2LiveLeaguePlayer
    {
        /// <summary>
        /// Gets or sets the 32 bit account id of the player.
        /// </summary>
        /// <value>
        /// The account id.
        /// </value>
        public uint account_id { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the id of the hero the player is on, 0 before the hero is picked.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

        /// <summary>
        /// Gets or sets the team the player belongs to. 0 is the Radiant, 1 is the Dire,
        /// 2 is a broadcaster and 4 is an unassigned player.
        /// </summary>
        /// <value>
        /// The team.
        /// </value>
        public int team { get; set; }

        /// <summary>
        /// Gets a value indicating whether this entry is a broadcaster rather than a player.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this entry is a broadcaster; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool isBroadcaster => team == 2;

        /// <summary>
        /// Gets the 64 bit steam id of the player.
        /// </summary>
        /// <value>
        /// The 64 bit steam id of the player.
        /// </value>
        [JsonIgnore]
        public long steamId64 => Dota2Ids.ToSteamId64(account_id);
    }

    /// <summary>
    /// Describes one of the two teams in a Dota 2 live league game.
    /// </summary>
    public class Dota2LiveLeagueTeam
    {
        /// <summary>
        /// Gets or sets the team id.
        /// </summary>
        /// <value>
        /// The team id.
        /// </value>
        public uint team_id { get; set; }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        /// <value>
        /// The name of the team.
        /// </value>
        public string team_name { get; set; }

        /// <summary>
        /// Gets or sets the ugc id of the team logo.
        /// </summary>
        /// <value>
        /// The team logo.
        /// </value>
        public ulong team_logo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether every member of the team is in the game.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the team is complete; otherwise, <c>false</c>.
        /// </value>
        public bool complete { get; set; }
    }

    /// <summary>
    /// Describes the live state of a Dota 2 league game.
    /// </summary>
    public class Dota2Scoreboard
    {
        /// <summary>
        /// Gets or sets the number of seconds the game has been running for.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public float duration { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds until Roshan respawns, 0 when Roshan is alive.
        /// </summary>
        /// <value>
        /// The Roshan respawn timer.
        /// </value>
        public float roshan_respawn_timer { get; set; }

        /// <summary>
        /// Gets or sets the state of the Radiant team.
        /// </summary>
        /// <value>
        /// The Radiant team state.
        /// </value>
        public Dota2ScoreboardTeam radiant { get; set; }

        /// <summary>
        /// Gets or sets the state of the Dire team.
        /// </summary>
        /// <value>
        /// The Dire team state.
        /// </value>
        public Dota2ScoreboardTeam dire { get; set; }

        /// <summary>
        /// Gets the length of time the game has been running for.
        /// </summary>
        /// <value>
        /// The length of time the game has been running for.
        /// </value>
        [JsonIgnore]
        public TimeSpan durationTime => TimeSpan.FromSeconds(duration);
    }

    /// <summary>
    /// Describes the live state of one of the two teams in a Dota 2 league game.
    /// </summary>
    public class Dota2ScoreboardTeam
    {
        /// <summary>
        /// Gets or sets the number of kills the team has scored.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int score { get; set; }

        /// <summary>
        /// Gets or sets the bit mask of the towers the team has left standing.
        /// See <see cref="towerStatus"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The tower state.
        /// </value>
        public int tower_state { get; set; }

        /// <summary>
        /// Gets or sets the bit mask of the barracks the team has left standing.
        /// See <see cref="barracksStatus"/> for the parsed value.
        /// </summary>
        /// <value>
        /// The barracks state.
        /// </value>
        public int barracks_state { get; set; }

        /// <summary>
        /// Gets or sets the live state of each player on the team.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<Dota2ScoreboardPlayer> players { get; set; }

        /// <summary>
        /// Gets or sets the abilities the team has levelled, only returned in Ability Draft.
        /// </summary>
        /// <value>
        /// The abilities.
        /// </value>
        public List<Dota2ScoreboardAbility> abilities { get; set; }

        /// <summary>
        /// Gets or sets the heroes the team has picked, only returned for drafted game modes.
        /// </summary>
        /// <value>
        /// The picks.
        /// </value>
        public List<Dota2HeroSelection> picks { get; set; }

        /// <summary>
        /// Gets or sets the heroes the team has banned, only returned for drafted game modes.
        /// </summary>
        /// <value>
        /// The bans.
        /// </value>
        public List<Dota2HeroSelection> bans { get; set; }

        /// <summary>
        /// Gets the towers the team has left standing.
        /// </summary>
        /// <value>
        /// The towers the team has left standing.
        /// </value>
        [JsonIgnore]
        public Dota2TowerStatus towerStatus => (Dota2TowerStatus)tower_state;

        /// <summary>
        /// Gets the barracks the team has left standing.
        /// </summary>
        /// <value>
        /// The barracks the team has left standing.
        /// </value>
        [JsonIgnore]
        public Dota2BarracksStatus barracksStatus => (Dota2BarracksStatus)barracks_state;
    }

    /// <summary>
    /// Describes the live state of a single player in a Dota 2 league game.
    /// </summary>
    public class Dota2ScoreboardPlayer
    {
        /// <summary>
        /// Gets or sets the 32 bit account id of the player.
        /// </summary>
        /// <value>
        /// The account id.
        /// </value>
        public uint account_id { get; set; }

        /// <summary>
        /// Gets or sets the player slot.
        /// </summary>
        /// <value>
        /// The player slot.
        /// </value>
        public int player_slot { get; set; }

        /// <summary>
        /// Gets or sets the id of the hero the player is on.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }

        /// <summary>
        /// Gets or sets the kills the player has scored.
        /// </summary>
        /// <value>
        /// The kills.
        /// </value>
        public int kills { get; set; }

        /// <summary>
        /// Gets or sets the number of times the player has died.
        /// The live scoreboard reports this as "death" rather than "deaths".
        /// </summary>
        /// <value>
        /// The deaths.
        /// </value>
        public int death { get; set; }

        /// <summary>
        /// Gets or sets the assists the player has recorded.
        /// </summary>
        /// <value>
        /// The assists.
        /// </value>
        public int assists { get; set; }

        /// <summary>
        /// Gets or sets the last hits the player has landed.
        /// </summary>
        /// <value>
        /// The last hits.
        /// </value>
        public int last_hits { get; set; }

        /// <summary>
        /// Gets or sets the denies the player has landed.
        /// </summary>
        /// <value>
        /// The denies.
        /// </value>
        public int denies { get; set; }

        /// <summary>
        /// Gets or sets the gold the player is currently holding.
        /// </summary>
        /// <value>
        /// The gold.
        /// </value>
        public int gold { get; set; }

        /// <summary>
        /// Gets or sets the level the player has reached.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int level { get; set; }

        /// <summary>
        /// Gets or sets the average gold per minute the player has earned.
        /// </summary>
        /// <value>
        /// The gold per minute.
        /// </value>
        public int gold_per_min { get; set; }

        /// <summary>
        /// Gets or sets the average experience per minute the player has earned.
        /// </summary>
        /// <value>
        /// The experience per minute.
        /// </value>
        public int xp_per_min { get; set; }

        /// <summary>
        /// Gets or sets the net worth of the player.
        /// </summary>
        /// <value>
        /// The net worth.
        /// </value>
        public int net_worth { get; set; }

        /// <summary>
        /// Gets or sets the state of the ultimate of the player.
        /// </summary>
        /// <value>
        /// The ultimate state.
        /// </value>
        public int ultimate_state { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds left on the ultimate cooldown of the player.
        /// </summary>
        /// <value>
        /// The ultimate cooldown.
        /// </value>
        public int ultimate_cooldown { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds until the player respawns, 0 when the player is alive.
        /// </summary>
        /// <value>
        /// The respawn timer.
        /// </value>
        public int respawn_timer { get; set; }

        /// <summary>
        /// Gets or sets the x coordinate of the player on the map.
        /// </summary>
        /// <value>
        /// The x coordinate.
        /// </value>
        public float position_x { get; set; }

        /// <summary>
        /// Gets or sets the y coordinate of the player on the map.
        /// </summary>
        /// <value>
        /// The y coordinate.
        /// </value>
        public float position_y { get; set; }

        /// <summary>
        /// Gets or sets the item in the first inventory slot.
        /// The live scoreboard reports the inventory slots as "item0" through "item5".
        /// </summary>
        /// <value>
        /// The item in the first inventory slot.
        /// </value>
        public int item0 { get; set; }

        /// <summary>
        /// Gets or sets the item in the second inventory slot.
        /// </summary>
        /// <value>
        /// The item in the second inventory slot.
        /// </value>
        public int item1 { get; set; }

        /// <summary>
        /// Gets or sets the item in the third inventory slot.
        /// </summary>
        /// <value>
        /// The item in the third inventory slot.
        /// </value>
        public int item2 { get; set; }

        /// <summary>
        /// Gets or sets the item in the fourth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the fourth inventory slot.
        /// </value>
        public int item3 { get; set; }

        /// <summary>
        /// Gets or sets the item in the fifth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the fifth inventory slot.
        /// </value>
        public int item4 { get; set; }

        /// <summary>
        /// Gets or sets the item in the sixth inventory slot.
        /// </summary>
        /// <value>
        /// The item in the sixth inventory slot.
        /// </value>
        public int item5 { get; set; }

        /// <summary>
        /// Gets the six inventory slots, in order. A slot holding no item reads 0.
        /// </summary>
        /// <value>
        /// The inventory slots.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<int> items => new[] { item0, item1, item2, item3, item4, item5 };

        /// <summary>
        /// Gets the 64 bit steam id of the player.
        /// </summary>
        /// <value>
        /// The 64 bit steam id of the player.
        /// </value>
        [JsonIgnore]
        public long steamId64 => Dota2Ids.ToSteamId64(account_id);

        /// <summary>
        /// Gets a value indicating whether the player is currently dead.
        /// </summary>
        /// <value>
        ///   <c>true</c> when the player is waiting to respawn; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool isDead => respawn_timer > 0;
    }

    /// <summary>
    /// Describes a levelled ability on the live scoreboard of an Ability Draft game.
    /// </summary>
    public class Dota2ScoreboardAbility
    {
        /// <summary>
        /// Gets or sets the id of the ability.
        /// </summary>
        /// <value>
        /// The ability id.
        /// </value>
        public int ability_id { get; set; }

        /// <summary>
        /// Gets or sets the level the ability has been taken to.
        /// </summary>
        /// <value>
        /// The ability level.
        /// </value>
        public int ability_level { get; set; }
    }

    /// <summary>
    /// Describes a hero picked or banned on the live scoreboard of a drafted game.
    /// </summary>
    public class Dota2HeroSelection
    {
        /// <summary>
        /// Gets or sets the id of the hero.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int hero_id { get; set; }
    }
}
