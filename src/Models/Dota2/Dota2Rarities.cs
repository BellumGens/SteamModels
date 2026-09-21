using System.Collections.Generic;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 cosmetic item rarity interface.
    /// GET: https://api.steampowered.com/IEconDOTA2_570/GetRarities/v1/?key=<API_KEY>&language=en&format=json
    /// </summary>
    public class Dota2Rarities
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2RaritiesResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 cosmetic item rarity response.
    /// </summary>
    public class Dota2RaritiesResult
    {
        /// <summary>
        /// Gets or sets the rarities.
        /// </summary>
        /// <value>
        /// The rarities.
        /// </value>
        public List<Dota2Rarity> rarities { get; set; }

        /// <summary>
        /// Gets or sets the number of rarities returned.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int count { get; set; }

        /// <summary>
        /// Gets or sets the status of the request. 200 means success.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }
    }

    /// <summary>
    /// Describes a single Dota 2 cosmetic item rarity tier.
    /// </summary>
    public class Dota2Rarity
    {
        /// <summary>
        /// Gets or sets the rarity id.
        /// </summary>
        /// <value>
        /// The rarity id.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the internal name of the rarity, e.g. "immortal".
        /// </summary>
        /// <value>
        /// The internal name of the rarity.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the display name of the rarity in the requested language, e.g. "Immortal".
        /// </summary>
        /// <value>
        /// The display name of the rarity.
        /// </value>
        public string localized_name { get; set; }

        /// <summary>
        /// Gets or sets the position of the rarity in the rarity order, lowest is most common.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int order { get; set; }

        /// <summary>
        /// Gets or sets the hex colour the rarity is rendered in, e.g. "#e4ae33".
        /// </summary>
        /// <value>
        /// The colour.
        /// </value>
        public string color { get; set; }
    }
}
