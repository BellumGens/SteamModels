using System.Xml.Serialization;

namespace SteamModels.Tests
{
    public class SteamUserTests
    {
        private const string Profile = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <profile>
          <steamID64>76561197960435530</steamID64>
          <steamID><![CDATA[Robin]]></steamID>
          <onlineState>in-game</onlineState>
          <stateMessage><![CDATA[In-Game<br/>Counter-Strike 2]]></stateMessage>
          <privacyState>public</privacyState>
          <visibilityState>3</visibilityState>
          <avatarIcon><![CDATA[https://avatars.steamstatic.com/a.jpg]]></avatarIcon>
          <avatarMedium><![CDATA[https://avatars.steamstatic.com/m.jpg]]></avatarMedium>
          <avatarFull><![CDATA[https://avatars.steamstatic.com/f.jpg]]></avatarFull>
          <vacBanned>0</vacBanned>
          <tradeBanState>None</tradeBanState>
          <isLimitedAccount>0</isLimitedAccount>
          <customURL><![CDATA[robinwalker]]></customURL>
          <memberSince>September 12, 2003</memberSince>
          <steamRating/>
          <hoursPlayed2Wk>12.5</hoursPlayed2Wk>
          <headline><![CDATA[Half-Life]]></headline>
          <location><![CDATA[Bellevue, Washington, United States]]></location>
          <realname><![CDATA[Robin Walker]]></realname>
          <summary><![CDATA[Works at Valve.]]></summary>
          <inGameServerIP>1.2.3.4:27015</inGameServerIP>
          <inGameInfo>
            <gameName><![CDATA[Counter-Strike 2]]></gameName>
            <gameLink><![CDATA[https://steamcommunity.com/app/730]]></gameLink>
            <gameIcon><![CDATA[https://media.steampowered.com/icon.jpg]]></gameIcon>
            <gameLogo><![CDATA[https://media.steampowered.com/logo.jpg]]></gameLogo>
            <gameLogoSmall><![CDATA[https://media.steampowered.com/logo_small.jpg]]></gameLogoSmall>
          </inGameInfo>
          <mostPlayedGames>
            <mostPlayedGame>
              <gameName><![CDATA[Counter-Strike 2]]></gameName>
              <gameLink>https://steamcommunity.com/app/730</gameLink>
              <gameIcon>https://media.steampowered.com/icon.jpg</gameIcon>
              <gameLogo>https://media.steampowered.com/logo.jpg</gameLogo>
              <gameLogoSmall>https://media.steampowered.com/logo_small.jpg</gameLogoSmall>
              <hoursPlayed>12.5</hoursPlayed>
              <hoursOnRecord>1,234</hoursOnRecord>
              <statsName>CSGO</statsName>
            </mostPlayedGame>
          </mostPlayedGames>
          <groups>
            <group isPrimary="1">
              <groupID64>103582791429521412</groupID64>
              <groupName><![CDATA[Valve]]></groupName>
              <groupURL><![CDATA[Valve]]></groupURL>
              <headline><![CDATA[]]></headline>
              <summary><![CDATA[]]></summary>
              <avatarIcon>https://avatars.steamstatic.com/g.jpg</avatarIcon>
              <avatarMedium>https://avatars.steamstatic.com/gm.jpg</avatarMedium>
              <avatarFull>https://avatars.steamstatic.com/gf.jpg</avatarFull>
              <memberCount>4000000</memberCount>
              <membersInChat>0</membersInChat>
              <membersInGame>1000</membersInGame>
              <membersOnLine>50000</membersOnLine>
            </group>
          </groups>
          <weblinks>
            <weblink>
              <title><![CDATA[Valve]]></title>
              <link><![CDATA[https://www.valvesoftware.com]]></link>
            </weblink>
          </weblinks>
        </profile>
        """;

        private static SteamUser Parse(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SteamUser));
            using StringReader reader = new StringReader(xml);
            return (SteamUser)serializer.Deserialize(reader);
        }

        [Fact]
        public void Deserializes_the_profile_fields()
        {
            SteamUser user = Parse(Profile);

            Assert.Equal("76561197960435530", user.steamID64);
            Assert.Equal("Robin", user.steamID);
            Assert.Equal("in-game", user.onlineState);
            Assert.Equal("public", user.privacyState);
            Assert.Equal(3, user.visibilityState);
            Assert.Equal(0, user.vacBanned);
            Assert.Equal("None", user.tradeBanState);
            Assert.Equal("robinwalker", user.customURL);
            Assert.Equal("September 12, 2003", user.memberSince);
            Assert.Equal(12.5f, user.hoursPlayed2Wk);
            Assert.Equal("Robin Walker", user.realname);
            Assert.Equal("Works at Valve.", user.summary);
        }

        [Fact]
        public void Country_is_taken_from_the_last_part_of_the_location()
        {
            Assert.Equal("United States", Parse(Profile).country);
        }

        [Fact]
        public void Country_tolerates_a_missing_location()
        {
            Assert.Null(new SteamUser().country);
            Assert.Equal("Bulgaria", new SteamUser { location = "Bulgaria" }.country);
        }

        [Fact]
        public void Deserializes_the_in_game_info()
        {
            SteamUser user = Parse(Profile);

            Assert.Equal("1.2.3.4:27015", user.inGameServerIP);
            Assert.NotNull(user.inGameInfo);
            Assert.Equal("Counter-Strike 2", user.inGameInfo.gameName);
            Assert.Equal("https://steamcommunity.com/app/730", user.inGameInfo.gameLink);
            Assert.Equal("https://media.steampowered.com/icon.jpg", user.inGameInfo.gameIcon);
            Assert.Equal("https://media.steampowered.com/logo.jpg", user.inGameInfo.gameLogo);
            Assert.Equal("https://media.steampowered.com/logo_small.jpg", user.inGameInfo.gameLogoSmall);
        }

        [Fact]
        public void Deserializes_the_web_links()
        {
            SteamUserWeblink link = Assert.Single(Parse(Profile).weblinks);

            Assert.Equal("Valve", link.title);
            Assert.Equal("https://www.valvesoftware.com", link.link);
        }

        [Fact]
        public void Deserializes_the_most_played_games()
        {
            MostPlayedGame game = Assert.Single(Parse(Profile).mostPlayedGames);

            Assert.Equal("Counter-Strike 2", game.gameName);
            Assert.Equal(12.5f, game.hoursPlayed);
            // the steam api groups the thousands, which is why this one is a string
            Assert.Equal("1,234", game.hoursOnRecord);
            Assert.Equal("CSGO", game.statsName);
        }

        [Fact]
        public void Deserializes_the_groups()
        {
            SteamUserGroup group = Assert.Single(Parse(Profile).groups);

            Assert.True(group.isPrimary);
            Assert.Equal("103582791429521412", group.groupID64);
            Assert.Equal("Valve", group.groupName);
            Assert.Equal(4000000, group.memberCount);
            Assert.Equal(1000, group.membersInGame);
            Assert.Equal(50000, group.membersOnLine);
        }

        [Fact]
        public void A_profile_that_is_not_in_game_has_no_in_game_info()
        {
            SteamUser user = Parse("""
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <profile>
              <steamID64>76561197960435530</steamID64>
              <steamID><![CDATA[Robin]]></steamID>
              <onlineState>offline</onlineState>
              <visibilityState>3</visibilityState>
            </profile>
            """);

            Assert.Equal("offline", user.onlineState);
            Assert.Null(user.inGameInfo);
            Assert.Null(user.inGameServerIP);
        }

        [Fact]
        public void Absent_collections_come_back_empty_rather_than_null()
        {
            // XmlSerializer instantiates every List member whether or not the containing
            // element was in the payload, so the XML models never hand back a null collection.
            SteamUser user = Parse("""
            <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            <profile>
              <steamID64>76561197960435530</steamID64>
              <onlineState>offline</onlineState>
            </profile>
            """);

            Assert.Empty(user.weblinks);
            Assert.Empty(user.mostPlayedGames);
            Assert.Empty(user.groups);
        }
    }
}
