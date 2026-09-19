using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 in game item list interface.
    /// GET: https://api.steampowered.com/IEconDOTA2_570/GetGameItems/v1/?key=<API_KEY>&language=en&format=json
    /// </summary>
    public class Dota2GameItems
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2GameItemsResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 in game item list response.
    /// </summary>
    public class Dota2GameItemsResult
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<Dota2GameItem> items { get; set; }

        /// <summary>
        /// Gets or sets the status of the request. 200 means success.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }
    }

    /// <summary>
    /// Describes a single Dota 2 in game item. The <see cref="id"/> is what the item slots of
    /// <see cref="Dota2MatchPlayer"/> refer to.
    /// </summary>
    public class Dota2GameItem
    {
        /// <summary>
        /// Gets or sets the item id.
        /// </summary>
        /// <value>
        /// The item id.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the internal name of the item, e.g. "item_blink".
        /// </summary>
        /// <value>
        /// The internal name of the item.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the gold cost of the item, 0 for items that cannot be bought.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public int cost { get; set; }

        /// <summary>
        /// Gets or sets whether the item is sold in the secret shop, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the item is sold in the secret shop.
        /// </value>
        public int secret_shop { get; set; }

        /// <summary>
        /// Gets or sets whether the item is sold in the side shop, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the item is sold in the side shop.
        /// </value>
        public int side_shop { get; set; }

        /// <summary>
        /// Gets or sets whether the item is a recipe, 1 for true.
        /// </summary>
        /// <value>
        /// Whether the item is a recipe.
        /// </value>
        public int recipe { get; set; }

        /// <summary>
        /// Gets or sets the display name of the item in the requested language, e.g. "Blink Dagger".
        /// Only returned when a language is passed to the interface.
        /// </summary>
        /// <value>
        /// The display name of the item.
        /// </value>
        public string localized_name { get; set; }

        /// <summary>
        /// Gets a value indicating whether the item is a recipe.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the item is a recipe; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool isRecipe => recipe == 1;

        /// <summary>
        /// Gets the internal name of the item without the "item_" prefix, e.g. "blink".
        /// This is the token the Steam cdn builds item icon urls from, currently
        /// https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/items/{shortName}.png
        /// </summary>
        /// <value>
        /// The internal name of the item without its prefix.
        /// </value>
        [JsonIgnore]
        public string shortName => name != null && name.StartsWith("item_")
            ? name.Substring("item_".Length)
            : name;
    }
}
