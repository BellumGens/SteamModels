using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2IdsTests
    {
        [Fact]
        public void AppId_is_570()
        {
            Assert.Equal(570, Dota2Ids.AppId);
        }

        [Theory]
        [InlineData(0u, 76561197960265728L)]
        [InlineData(1u, 76561197960265729L)]
        [InlineData(86745912u, 76561198047011640L)]
        public void ToSteamId64_adds_the_individual_account_offset(uint accountId, long expected)
        {
            Assert.Equal(expected, Dota2Ids.ToSteamId64(accountId));
        }

        [Fact]
        public void ToAccountId_round_trips_ToSteamId64()
        {
            Assert.Equal(Payloads.AccountId, Dota2Ids.ToAccountId(Dota2Ids.ToSteamId64(Payloads.AccountId)));
        }

        [Fact]
        public void ToAccountId_rejects_ids_below_the_individual_range()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Dota2Ids.ToAccountId(1234));
        }

        [Fact]
        public void TryGetAccountId_parses_the_string_form_the_steam_api_returns()
        {
            Assert.True(Dota2Ids.TryGetAccountId("76561198047011640", out uint accountId));
            Assert.Equal(Payloads.AccountId, accountId);
        }

        [Theory]
        [InlineData("not-a-number")]
        [InlineData("1234")]
        [InlineData("")]
        public void TryGetAccountId_returns_false_rather_than_throwing(string steamId64)
        {
            Assert.False(Dota2Ids.TryGetAccountId(steamId64, out uint accountId));
            Assert.Equal(0u, accountId);
        }

        [Theory]
        [InlineData(0, Dota2Team.Radiant, 0)]
        [InlineData(4, Dota2Team.Radiant, 4)]
        [InlineData(128, Dota2Team.Dire, 0)]
        [InlineData(131, Dota2Team.Dire, 3)]
        [InlineData(132, Dota2Team.Dire, 4)]
        public void Player_slots_decode_into_a_team_and_a_position(int playerSlot, Dota2Team team, int slot)
        {
            Assert.Equal(team, Dota2Ids.GetTeam(playerSlot));
            Assert.Equal(slot, Dota2Ids.GetSlot(playerSlot));
        }
    }
}
