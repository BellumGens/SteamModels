using System.Xml.Serialization;

namespace SteamModels.Tests
{
    public class SteamGroupTests
    {
        private const string MemberList = """
        <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
        <memberList>
          <groupID64>103582791429521412</groupID64>
          <groupDetails>
            <groupName><![CDATA[Valve]]></groupName>
            <groupURL><![CDATA[Valve]]></groupURL>
            <headline><![CDATA[Headline]]></headline>
            <summary><![CDATA[Summary]]></summary>
            <avatarIcon>https://avatars.steamstatic.com/g.jpg</avatarIcon>
            <avatarMedium>https://avatars.steamstatic.com/gm.jpg</avatarMedium>
            <avatarFull>https://avatars.steamstatic.com/gf.jpg</avatarFull>
            <memberCount>4000000</memberCount>
            <membersInChat>3</membersInChat>
            <membersInGame>1000</membersInGame>
            <membersOnline>50000</membersOnline>
          </groupDetails>
          <memberCount>4000000</memberCount>
          <totalPages>4000</totalPages>
          <currentPage>1</currentPage>
          <startingMember>0</startingMember>
          <members>
            <steamID64>76561197960435530</steamID64>
            <steamID64>76561197960435531</steamID64>
          </members>
        </memberList>
        """;

        private static SteamGroup Parse(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SteamGroup));
            using StringReader reader = new StringReader(xml);
            return (SteamGroup)serializer.Deserialize(reader);
        }

        [Fact]
        public void Deserializes_the_paging_fields()
        {
            SteamGroup group = Parse(MemberList);

            Assert.Equal("103582791429521412", group.groupID64);
            Assert.Equal(4000000, group.memberCount);
            Assert.Equal(4000, group.totalPages);
            Assert.Equal(1, group.currentPage);
            Assert.Equal(0, group.startingMember);
        }

        [Fact]
        public void Deserializes_the_group_details()
        {
            SteamGroupDetails details = Parse(MemberList).groupDetails;

            Assert.Equal("Valve", details.groupName);
            Assert.Equal("Valve", details.groupURL);
            Assert.Equal("Headline", details.headline);
            Assert.Equal("Summary", details.summary);
            Assert.Equal(4000000, details.memberCount);
            Assert.Equal(3, details.membersInChat);
            Assert.Equal(1000, details.membersInGame);
            Assert.Equal(50000, details.membersOnline);
        }

        [Fact]
        public void Deserializes_the_members_as_steam_ids()
        {
            SteamGroup group = Parse(MemberList);

            Assert.Equal(new[] { "76561197960435530", "76561197960435531" }, group.members);
        }
    }
}
