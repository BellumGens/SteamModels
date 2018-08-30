using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SteamModels.Models
{
    /// <summary>
    /// Describes a SteamGroup
    /// GET: https://steamcommunity.com/gid/<GROUP_ID64>/memberslistxml/?xml=1
    /// GET: https://steamcommunity.com/groups/<GROUP_URL>/memberslistxml/?xml=1
    /// </summary>
    [XmlRoot("memberList")]
    class SteamGroup
    {
        /// <summary>
        /// Gets or sets the group id 64.
        /// </summary>
        /// <value>
        /// The group id 64.
        /// </value>
        public string groupID64 { get; set; }

        /// <summary>
        /// Gets or sets the member count.
        /// </summary>
        /// <value>
        /// The member count.
        /// </value>
        public int memberCount { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int totalPages { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int currentPage { get; set; }

        /// <summary>
        /// Gets or sets the starting member.
        /// </summary>
        /// <value>
        /// The starting member.
        /// </value>
        public int startingMember { get; set; }

        public SteamGroupDetails groupDetails { get; set; }

        [XmlArrayItem("steamID64")]
        public SteamGroupMembers members { get; set; }
    }

    /// <summary>
    /// Describes the Steam group members by their steamID64
    /// </summary>
    public class SteamGroupMembers
    {
        public string steamID64 { get; set; }
    }

    /// <summary>
    /// Describes the Steam group details
    /// </summary>
    public class SteamGroupDetails
    {
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        public string groupName { get; set; }

        /// <summary>
        /// Gets or sets the group URL.
        /// </summary>
        /// <value>
        /// The group URL.
        /// </value>
        public string groupURL { get; set; }

        /// <summary>
        /// Gets or sets the headline.
        /// </summary>
        /// <value>
        /// The headline.
        /// </value>
        public string headline { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string summary { get; set; }

        /// <summary>
        /// Gets or sets the avatar icon.
        /// </summary>
        /// <value>
        /// The avatar icon.
        /// </value>
        public string avatarIcon { get; set; }

        /// <summary>
        /// Gets or sets the avatar medium.
        /// </summary>
        /// <value>
        /// The avatar medium.
        /// </value>
        public string avatarMedium { get; set; }

        /// <summary>
        /// Gets or sets the avatar full.
        /// </summary>
        /// <value>
        /// The avatar full.
        /// </value>
        public string avatarFull { get; set; }

        /// <summary>
        /// Gets or sets the member count.
        /// </summary>
        /// <value>
        /// The member count.
        /// </value>
        public int memberCount { get; set; }

        /// <summary>
        /// Gets or sets the members in chat.
        /// </summary>
        /// <value>
        /// The members in chat.
        /// </value>
        public int membersInChat { get; set; }

        /// <summary>
        /// Gets or sets the members in game.
        /// </summary>
        /// <value>
        /// The members in game.
        /// </value>
        public int membersInGame { get; set; }

        /// <summary>
        /// Gets or sets the members online.
        /// </summary>
        /// <value>
        /// The members online.
        /// </value>
        public int membersOnline { get; set; }
    }
}
