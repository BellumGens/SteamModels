using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 team info interface.
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetTeamInfoByTeamID/v1/?key=<API_KEY>&start_at_team_id=<TEAM_ID>&teams_requested=1&format=json
    /// </summary>
    public class Dota2TeamInfo
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2TeamInfoResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 team info response.
    /// </summary>
    public class Dota2TeamInfoResult
    {
        /// <summary>
        /// Gets or sets the status of the request. 1 means success.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }

        /// <summary>
        /// Gets or sets the teams.
        /// </summary>
        /// <value>
        /// The teams.
        /// </value>
        public List<Dota2TeamDetails> teams { get; set; }
    }

    /// <summary>
    /// Describes a single professional Dota 2 team.
    /// The Steam API does not echo the team id back in this response, so it has to be tracked
    /// from the start_at_team_id the request was made with.
    /// </summary>
    public class Dota2TeamDetails
    {
        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        /// <value>
        /// The name of the team.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the tag of the team.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string tag { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation of the team, e.g. "SEC".
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        public string abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the unix timestamp the team was created at.
        /// </summary>
        /// <value>
        /// The time created.
        /// </value>
        public long time_created { get; set; }

        /// <summary>
        /// Gets or sets the ugc id of the team logo.
        /// </summary>
        /// <value>
        /// The logo.
        /// </value>
        public ulong logo { get; set; }

        /// <summary>
        /// Gets or sets the ugc id of the team sponsor logo.
        /// </summary>
        /// <value>
        /// The sponsor logo.
        /// </value>
        public ulong logo_sponsor { get; set; }

        /// <summary>
        /// Gets or sets the two letter country code of the team.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string country_code { get; set; }

        /// <summary>
        /// Gets or sets the url of the team website.
        /// </summary>
        /// <value>
        /// The url.
        /// </value>
        public string url { get; set; }

        /// <summary>
        /// Gets or sets the number of games the team has played.
        /// </summary>
        /// <value>
        /// The games played.
        /// </value>
        public int games_played { get; set; }

        /// <summary>
        /// Gets or sets the account id of the team administrator.
        /// </summary>
        /// <value>
        /// The admin account id.
        /// </value>
        public uint admin_account_id { get; set; }

        /// <summary>
        /// Gets or sets the account id of the first team member.
        /// See <see cref="playerAccountIds"/> for the full roster.
        /// </summary>
        /// <value>
        /// The account id of the first team member.
        /// </value>
        public uint player_0_account_id { get; set; }

        /// <summary>
        /// Gets or sets the account id of the second team member.
        /// </summary>
        /// <value>
        /// The account id of the second team member.
        /// </value>
        public uint player_1_account_id { get; set; }

        /// <summary>
        /// Gets or sets the account id of the third team member.
        /// </summary>
        /// <value>
        /// The account id of the third team member.
        /// </value>
        public uint player_2_account_id { get; set; }

        /// <summary>
        /// Gets or sets the account id of the fourth team member.
        /// </summary>
        /// <value>
        /// The account id of the fourth team member.
        /// </value>
        public uint player_3_account_id { get; set; }

        /// <summary>
        /// Gets or sets the account id of the fifth team member.
        /// </summary>
        /// <value>
        /// The account id of the fifth team member.
        /// </value>
        public uint player_4_account_id { get; set; }

        /// <summary>
        /// Gets or sets the account id of the sixth team member, i.e. the first stand in.
        /// </summary>
        /// <value>
        /// The account id of the sixth team member.
        /// </value>
        public uint player_5_account_id { get; set; }

        /// <summary>
        /// Gets or sets the account id of the seventh team member, i.e. the second stand in.
        /// </summary>
        /// <value>
        /// The account id of the seventh team member.
        /// </value>
        public uint player_6_account_id { get; set; }

        /// <summary>
        /// Gets the time the team was created at.
        /// </summary>
        /// <value>
        /// The time the team was created at.
        /// </value>
        [JsonIgnore]
        public DateTimeOffset timeCreated => DateTimeOffset.FromUnixTimeSeconds(time_created);

        /// <summary>
        /// Gets the account ids of the team roster, skipping the slots the Steam API left empty.
        /// </summary>
        /// <value>
        /// The account ids of the team roster.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<uint> playerAccountIds => new[]
        {
            player_0_account_id,
            player_1_account_id,
            player_2_account_id,
            player_3_account_id,
            player_4_account_id,
            player_5_account_id,
            player_6_account_id
        }.Where(id => id != 0).ToList();
    }
}
