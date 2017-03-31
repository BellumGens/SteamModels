using System;
using System.Xml.Serialization;

namespace SteamModels
{
    public class SteamGroup
    {
        /// <summary>
        /// Gets or sets a value indicating whether this group is primary for the <see cref="SteamUser"/>.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this group is primary group for the <see cref="SteamUser"/>; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute("isPrimary")]
        public bool isPrimary { get; set; }

        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        /// <value>
        /// The group id.
        /// </value>
        public string groupID64 { get; set; }

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
        /// Gets or sets the group headline.
        /// </summary>
        /// <value>
        /// The group headline.
        /// </value>
        public string headline { get; set; }

        /// <summary>
        /// Gets or sets the group summary.
        /// </summary>
        /// <value>
        /// The group summary.
        /// </value>
        public string summary { get; set; }

        /// <summary>
        /// Gets or sets the group avatar icon.
        /// </summary>
        /// <value>
        /// The group avatar icon.
        /// </value>
        public string avatarIcon { get; set; }

        /// <summary>
        /// Gets or sets the group avatar icon medium.
        /// </summary>
        /// <value>
        /// The group avatar icon medium.
        /// </value>
        public string avatarMedium { get; set; }

        /// <summary>
        /// Gets or sets the group avatar icon full.
        /// </summary>
        /// <value>
        /// The group avatar icon full.
        /// </value>
        public string avatarFull { get; set; }

        /// <summary>
        /// Gets or sets the group member count.
        /// </summary>
        /// <value>
        /// The group member count.
        /// </value>
        public int memberCount { get; set; }

        /// <summary>
        /// Gets or sets the group members in chat.
        /// </summary>
        /// <value>
        /// The group members in chat.
        /// </value>
        public int membersInChat { get; set; }

        /// <summary>
        /// Gets or sets the group members in game.
        /// </summary>
        /// <value>
        /// The group members in game.
        /// </value>
        public int membersInGame { get; set; }

        /// <summary>
        /// Gets or sets the group members online.
        /// </summary>
        /// <value>
        /// The group members online.
        /// </value>
        public int membersOnLine { get; set; }
    }
}