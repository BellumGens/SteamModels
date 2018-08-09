using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamModels
{
    /// <summary>
    /// Describing steam user profile
    /// GET: http://steamcommunity.com/profiles/<STEAMID64>/?xml=1
    /// GET: http://steamcommunity.com/id/<CUSTOMURL>/?xml=1
    /// </summary>
    [XmlRoot("profile")]
    public class SteamUser
    {
        /// <summary>
        /// Gets or sets the steam id.
        /// </summary>
        /// <value>
        /// The steam id.
        /// </value>
        public string steamID64 { get; set; }

        /// <summary>
        /// Gets or sets the steam identifier.
        /// </summary>
        /// <value>
        /// The steam identifier.
        /// </value>
        public string steamID { get; set; }

        /// <summary>
        /// Gets or sets the online state .
        /// </summary>
        /// <value>
        /// The online state.
        /// </value>
        public string onlineState { get; set; }

        /// <summary>
        /// Gets or sets the state message.
        /// </summary>
        /// <value>
        /// The state message.
        /// </value>
        public string stateMessage { get; set; }

        /// <summary>
        /// Gets or sets the privacy state of the user.
        /// </summary>
        /// <value>
        /// The privacy state of the user.
        /// </value>
        public string privacyState { get; set; }

        /// <summary>
        /// Gets or sets the visibility state of the user.
        /// </summary>
        /// <value>
        /// The visibility state of the user.
        /// </value>
        public int visibilityState { get; set; }

        /// <summary>
        /// Gets or sets the avatar icon url.
        /// </summary>
        /// <value>
        /// The avatar icon url.
        /// </value>
        public string avatarIcon { get; set; }

        /// <summary>
        /// Gets or sets the medium avatar url.
        /// </summary>
        /// <value>
        /// The medium avatar url.
        /// </value>
        public string avatarMedium { get; set; }

        /// <summary>
        /// Gets or sets the full avatar url.
        /// </summary>
        /// <value>
        /// The full avatar url.
        /// </value>
        public string avatarFull { get; set; }

        /// <summary>
        /// Gets or sets the number of vac bans.
        /// </summary>
        /// <value>
        /// The number of vac bans.
        /// </value>
        public int vacBanned { get; set; }

        /// <summary>
        /// Gets or sets the trade ban state.
        /// </summary>
        /// <value>
        /// The trade ban state.
        /// </value>
        public string tradeBanState { get; set; }

        /// <summary>
        /// Gets or sets whether the account is limited.
        /// </summary>
        /// <value>
        /// Whether is limited account.
        /// </value>
        public int isLimitedAccount { get; set; }

        /// <summary>
        /// Gets or sets the custom URL.
        /// </summary>
        /// <value>
        /// The custom URL.
        /// </value>
        public string customURL { get; set; }

        /// <summary>
        /// Gets or sets since when the user is a member of steam.
        /// </summary>
        /// <value>
        /// Since when the user is a member of steam.
        /// </value>
        public string memberSince { get; set; }

        /// <summary>
        /// Gets or sets the hours played.
        /// </summary>
        /// <value>
        /// The hours played.
        /// </value>
        public float hoursPlayed2Wk { get; set; }

        /// <summary>
        /// Gets or sets the user account headline.
        /// </summary>
        /// <value>
        /// The headline.
        /// </value>
        public string headline { get; set; }

        /// <summary>
        /// Gets or sets the user location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string location { get; set; }

        /// <summary>
        /// Gets the user country, extracted from the user location.
        /// </summary>
        /// <value>
        /// The user country.
        /// </value>
        public string country {
            get
            {
                string[] parts = this.location.Split(',');
                return parts[parts.Length - 1].Trim();
            }
        }

        /// <summary>
        /// Gets or sets the user's realname.
        /// </summary>
        /// <value>
        /// The user's realname.
        /// </value>
        public string realname { get; set; }

        /// <summary>
        /// Gets or sets the user's profile summary.
        /// </summary>
        /// <value>
        /// The user's profile summary.
        /// </value>
        public string summary { get; set; }

        /// <summary>
        /// Gets or sets the most played games.
        /// </summary>
        /// <value>
        /// The most played games.
        /// </value>
        public List<MostPlayedGame> mostPlayedGames { get; set; }

        /// <summary>
        /// Gets or sets the steam groups the user is a member of.
        /// </summary>
        /// <value>
        /// The steam groups the user is a member of.
        /// </value>
        public List<SteamGroup> groups { get; set; }
    }
}
