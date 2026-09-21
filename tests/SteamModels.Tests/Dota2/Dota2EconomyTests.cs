using System.Text.Json;
using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2EconomyTests
    {
        [Fact]
        public void GetHeroes_deserializes_and_strips_the_hero_name_prefix()
        {
            Dota2HeroesResult result = JsonSerializer.Deserialize<Dota2Heroes>("""
            {"result":{"heroes":[
              {"name":"npc_dota_hero_antimage","id":1,"localized_name":"Anti-Mage"},
              {"name":"npc_dota_hero_queenofpain","id":39,"localized_name":"Queen of Pain"}],
              "status":200,"count":2}}
            """).result;

            Assert.Equal(200, result.status);
            Assert.Equal(2, result.count);
            Assert.Equal(1, result.heroes[0].id);
            Assert.Equal("Anti-Mage", result.heroes[0].localized_name);
            Assert.Equal("antimage", result.heroes[0].shortName);
            Assert.Equal("queenofpain", result.heroes[1].shortName);
        }

        [Fact]
        public void Hero_shortName_leaves_an_unexpected_name_alone()
        {
            Assert.Equal("something_else", new Dota2Hero { name = "something_else" }.shortName);
            Assert.Null(new Dota2Hero().shortName);
        }

        [Fact]
        public void GetRarities_deserializes()
        {
            Dota2RaritiesResult result = JsonSerializer.Deserialize<Dota2Rarities>("""
            {"result":{"rarities":[
              {"name":"common","id":1,"order":0,"color":"#b0c3d9","localized_name":"Common"},
              {"name":"immortal","id":6,"order":5,"color":"#e4ae33","localized_name":"Immortal"}],
              "count":2,"status":200}}
            """).result;

            Assert.Equal(200, result.status);
            Assert.Equal(2, result.count);
            Assert.Equal(1, result.rarities[0].id);
            Assert.Equal(0, result.rarities[0].order);
            Assert.Equal("immortal", result.rarities[1].name);
            Assert.Equal(5, result.rarities[1].order);
            Assert.Equal("#e4ae33", result.rarities[1].color);
            Assert.Equal("Immortal", result.rarities[1].localized_name);
        }

        [Fact]
        public void GetTournamentPrizePool_deserializes()
        {
            Dota2TournamentPrizePoolResult result =
                JsonSerializer.Deserialize<Dota2TournamentPrizePool>("""
                {"result":{"prize_pool":2664000,"league_id":16935,"status":200}}
                """).result;

            Assert.Equal(2664000L, result.prize_pool);
            Assert.Equal(16935, result.league_id);
            Assert.Equal(200, result.status);
        }
    }
}
