using System;

namespace SteamModels
{
    public class MostPlayedGame
    {
        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string gameName { get; set; }

        /// <summary>
        /// Gets or sets the game link.
        /// </summary>
        /// <value>
        /// The game link.
        /// </value>
        public string gameLink { get; set; }

        /// <summary>
        /// Gets or sets the game icon.
        /// </summary>
        /// <value>
        /// The game icon.
        /// </value>
        public string gameIcon { get; set; }

        /// <summary>
        /// Gets or sets the game logo.
        /// </summary>
        /// <value>
        /// The game logo.
        /// </value>
        public string gameLogo { get; set; }

        /// <summary>
        /// Gets or sets the game logo small.
        /// </summary>
        /// <value>
        /// The game logo small.
        /// </value>
        public string gameLogoSmall { get; set; }

        /// <summary>
        /// Gets or sets the hours the user has played the game for.
        /// </summary>
        /// <value>
        /// The hours the user has played the game for.
        /// </value>
        public float hoursPlayed { get; set; }

        /// <summary>
        /// Gets or sets the hours on record.
        /// </summary>
        /// <value>
        /// The hours on record.
        /// </value>
        public int hoursOnRecord { get; set; }

        /// <summary>
        /// Gets or sets the name of the stats.
        /// </summary>
        /// <value>
        /// The name of the stats.
        /// </value>
        public string statsName { get; set; }
    }
}