using System.Text.Json;

namespace SteamModels.Tests
{
    public class SteamNewsTests
    {
        private const string News = """
        {"appnews":{"appid":730,"newsitems":[
          {"gid":"5136871107920571879","title":"Release Notes","url":"https://steamcommunity.com/ogg/730/announcements/detail/1",
           "is_external_url":true,"author":"Valve","contents":"Fixed a bug.",
           "feedlabel":"Community Announcements","date":1745000000,
           "feedname":"steam_community_announcements","feed_type":1,"appid":730,
           "tags":["patchnotes"]}],
          "count":1}}
        """;

        [Fact]
        public void Deserializes_the_app_news()
        {
            SteamAppNews news = JsonSerializer.Deserialize<SteamNews>(News).appnews;

            Assert.Equal(730, news.appid);
            Assert.Equal(1, news.count);
            Assert.Single(news.newsitems);
        }

        [Fact]
        public void Deserializes_a_news_item()
        {
            NewsItem item = JsonSerializer.Deserialize<SteamNews>(News).appnews.newsitems[0];

            Assert.Equal("5136871107920571879", item.gid);
            Assert.Equal("Release Notes", item.title);
            Assert.True(item.is_external_url);
            Assert.Equal("Valve", item.author);
            Assert.Equal("Fixed a bug.", item.contents);
            Assert.Equal("Community Announcements", item.feedlabel);
            Assert.Equal(1745000000, item.date);
            Assert.Equal(1, item.feed_type);
            Assert.Equal(730, item.appid);
            Assert.Equal(new[] { "patchnotes" }, item.tags);
        }
    }
}
