using System.Collections.Generic;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 league listing interface.
    /// GET: https://api.steampowered.com/IDOTA2Match_570/GetLeagueListing/v1/?key=<API_KEY>&language=en&format=json
    /// </summary>
    public class Dota2LeagueListing
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2LeagueListingResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 league listing response.
    /// </summary>
    public class Dota2LeagueListingResult
    {
        /// <summary>
        /// Gets or sets the leagues.
        /// </summary>
        /// <value>
        /// The leagues.
        /// </value>
        public List<Dota2League> leagues { get; set; }
    }

    /// <summary>
    /// Describes a single Dota 2 league.
    /// </summary>
    public class Dota2League
    {
        /// <summary>
        /// Gets or sets the league id, which the leagueid field of
        /// <see cref="Dota2MatchDetailsResult"/> refers to.
        /// </summary>
        /// <value>
        /// The league id.
        /// </value>
        public int leagueid { get; set; }

        /// <summary>
        /// Gets or sets the name of the league.
        /// </summary>
        /// <value>
        /// The name of the league.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the description of the league.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string description { get; set; }

        /// <summary>
        /// Gets or sets the url of the league website.
        /// </summary>
        /// <value>
        /// The tournament url.
        /// </value>
        public string tournament_url { get; set; }

        /// <summary>
        /// Gets or sets the item definition index of the league ticket.
        /// </summary>
        /// <value>
        /// The item definition index.
        /// </value>
        public int itemdef { get; set; }
    }

    /// <summary>
    /// Class describing the response format of the Dota 2 tournament prize pool interface.
    /// GET: https://api.steampowered.com/IEconDOTA2_570/GetTournamentPrizePool/v1/?key=<API_KEY>&leagueid=<LEAGUE_ID>&format=json
    /// </summary>
    public class Dota2TournamentPrizePool
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2TournamentPrizePoolResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 tournament prize pool response.
    /// </summary>
    public class Dota2TournamentPrizePoolResult
    {
        /// <summary>
        /// Gets or sets the prize pool in US dollars.
        /// </summary>
        /// <value>
        /// The prize pool.
        /// </value>
        public long prize_pool { get; set; }

        /// <summary>
        /// Gets or sets the league id the prize pool is for.
        /// </summary>
        /// <value>
        /// The league id.
        /// </value>
        public int league_id { get; set; }

        /// <summary>
        /// Gets or sets the status of the request. 200 means success.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }
    }
}
