using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SteamModels
{
    /// <summary>
    /// Class describing the response format of the ISteamNews API interface.
    /// e.g. http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid=440&count=3&maxlength=300&format=json
    /// </summary>
    public class SteamNews
    {
        /// <summary>
        /// Gets or sets the steam app news.
        /// </summary>
        /// <value>
        /// The steam app news.
        /// </value>
        public SteamAppNews appnews { get; set; }
    }

    [XmlRoot("appnews")]
    public class SteamAppNews
    {
        /// <summary>
        /// Gets or sets the appid.
        /// </summary>
        /// <value>
        /// The appid.
        /// </value>
        public int appid { get; set; }

        /// <summary>
        /// Gets or sets the newsitems.
        /// </summary>
        /// <value>
        /// The newsitems.
        /// </value>
        public List<NewsItem> newsitems { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int count { get; set; }
    }
    
    public class NewsItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the news item.
        /// </summary>
        /// <value>
        /// The gid.
        /// </value>
        public string gid { get; set; }

        /// <summary>
        /// Gets or sets the title of the news item.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is external URL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is external URL; otherwise, <c>false</c>.
        /// </value>
        public bool is_external_url { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        public string author { get; set; }

        /// <summary>
        /// Gets or sets the news contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        public string contents { get; set; }

        /// <summary>
        /// Gets or sets the feed label.
        /// </summary>
        /// <value>
        /// The feedlabel.
        /// </value>
        public string feedlabel { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public int date { get; set; }

        /// <summary>
        /// Gets or sets the feed name.
        /// </summary>
        /// <value>
        /// The feedname.
        /// </value>
        public string feedname { get; set; }

        /// <summary>
        /// Gets or sets the type of the feed.
        /// </summary>
        /// <value>
        /// The type of the feed.
        /// </value>
        public int feed_type { get; set; }

        /// <summary>
        /// Gets or sets the appid of the app the news item is for.
        /// e.g. 730 will be for Counter Strike: Global Offensive
        /// </summary>
        /// <value>
        /// The appid of the app the news item is for.
        /// </value>
        public int appid { get; set; }
    }
}
