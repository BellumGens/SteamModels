using System.Text.Json;

namespace SteamModels.Tests
{
    public class SteamUserSummaryTests
    {
        private const string InGame = """
        {"response":{"players":[{"steamid":"76561197960435530","communityvisibilitystate":3,
        "profilestate":1,"personaname":"Robin","profileurl":"https://steamcommunity.com/id/robinwalker/",
        "avatar":"a.jpg","avatarmedium":"m.jpg","avatarfull":"f.jpg",
        "avatarhash":"81b5478529dce13bf24b55ac42c1af7058aaf7a9","lastlogoff":1745000000,
        "personastate":1,"realname":"Robin Walker","primaryclanid":"103582791429521412",
        "timecreated":1063407589,"personastateflags":0,"loccountrycode":"US","locstatecode":"WA",
        "loccityid":3961,"commentpermission":1,"gameid":"730","gameextrainfo":"Counter-Strike 2",
        "gameserverip":"1.2.3.4:27015","gameserversteamid":"90071992547409920",
        "lobbysteamid":"109775240981170934"}]}}
        """;

        private const string Private = """
        {"response":{"players":[{"steamid":"76561197960435530","communityvisibilitystate":1,
        "personaname":"Robin","profileurl":"https://steamcommunity.com/id/robinwalker/",
        "avatar":"a.jpg","avatarmedium":"m.jpg","avatarfull":"f.jpg","personastate":0}]}}
        """;

        private static SteamUserSummary Parse(string json) =>
            JsonSerializer.Deserialize<SteamUsersSummary>(json).response.players.Single();

        [Fact]
        public void Deserializes_the_long_standing_fields()
        {
            SteamUserSummary player = Parse(InGame);

            Assert.Equal("76561197960435530", player.steamid);
            Assert.Equal("Robin", player.personaname);
            Assert.Equal(3, player.communityvisibilitystate);
            Assert.Equal(1, player.profilestate);
            Assert.Equal(1063407589, player.timecreated);
            Assert.Equal("103582791429521412", player.primaryclanid);
            Assert.Equal("US", player.loccountrycode);
        }

        [Fact]
        public void Deserializes_the_profile_fields_the_steam_api_returns_today()
        {
            SteamUserSummary player = Parse(InGame);

            Assert.Equal("Robin Walker", player.realname);
            Assert.Equal("81b5478529dce13bf24b55ac42c1af7058aaf7a9", player.avatarhash);
            Assert.Equal("WA", player.locstatecode);
            Assert.Equal(3961, player.loccityid);
            Assert.Equal(1, player.commentpermission);
        }

        [Fact]
        public void Deserializes_the_in_game_fields()
        {
            SteamUserSummary player = Parse(InGame);

            Assert.Equal("730", player.gameid);
            Assert.Equal("Counter-Strike 2", player.gameextrainfo);
            Assert.Equal("1.2.3.4:27015", player.gameserverip);
            Assert.Equal("90071992547409920", player.gameserversteamid);
            Assert.Equal("109775240981170934", player.lobbysteamid);
        }

        [Fact]
        public void A_private_profile_leaves_the_optional_fields_empty()
        {
            SteamUserSummary player = Parse(Private);

            Assert.Equal(1, player.communityvisibilitystate);
            Assert.Null(player.realname);
            Assert.Null(player.gameid);
            Assert.Null(player.loccountrycode);
            Assert.Equal(0, player.timecreated);
        }
    }
}
