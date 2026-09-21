namespace SteamModels.Dota2
{
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
        /// Gets or sets the league id the prize pool is for, which the leagueid field of
        /// <see cref="Dota2MatchDetailsResult"/> refers to.
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
