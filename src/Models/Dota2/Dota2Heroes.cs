using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Class describing the response format of the Dota 2 hero list interface.
    /// GET: https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v1/?key=<API_KEY>&language=en&format=json
    /// </summary>
    public class Dota2Heroes
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Dota2HeroesResult result { get; set; }
    }

    /// <summary>
    /// Describes the payload of a Dota 2 hero list response.
    /// </summary>
    public class Dota2HeroesResult
    {
        /// <summary>
        /// Gets or sets the heroes.
        /// </summary>
        /// <value>
        /// The heroes.
        /// </value>
        public List<Dota2Hero> heroes { get; set; }

        /// <summary>
        /// Gets or sets the status of the request. 200 means success.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status { get; set; }

        /// <summary>
        /// Gets or sets the number of heroes returned.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int count { get; set; }
    }

    /// <summary>
    /// Describes a single Dota 2 hero. The <see cref="id"/> is what the hero_id fields of
    /// <see cref="Dota2MatchPlayer"/> and <see cref="Dota2PickBan"/> refer to.
    /// </summary>
    public class Dota2Hero
    {
        /// <summary>
        /// Gets or sets the hero id.
        /// </summary>
        /// <value>
        /// The hero id.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the internal name of the hero, e.g. "npc_dota_hero_antimage".
        /// </summary>
        /// <value>
        /// The internal name of the hero.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the display name of the hero in the requested language, e.g. "Anti-Mage".
        /// Only returned when a language is passed to the interface.
        /// </summary>
        /// <value>
        /// The display name of the hero.
        /// </value>
        public string localized_name { get; set; }

        /// <summary>
        /// Gets the internal name of the hero without the "npc_dota_hero_" prefix, e.g. "antimage".
        /// This is the token the Steam cdn builds hero portrait urls from, currently
        /// https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/heroes/{shortName}.png
        /// </summary>
        /// <value>
        /// The internal name of the hero without its prefix.
        /// </value>
        [JsonIgnore]
        public string shortName => name != null && name.StartsWith("npc_dota_hero_")
            ? name.Substring("npc_dota_hero_".Length)
            : name;
    }
}
