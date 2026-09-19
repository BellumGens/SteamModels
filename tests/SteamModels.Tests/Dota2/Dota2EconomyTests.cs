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
        public void GetGameItems_deserializes_and_flags_recipes()
        {
            Dota2GameItemsResult result = JsonSerializer.Deserialize<Dota2GameItems>("""
            {"result":{"items":[
              {"id":1,"name":"item_blink","cost":2250,"secret_shop":0,"side_shop":0,"recipe":0,
               "localized_name":"Blink Dagger"},
              {"id":40,"name":"item_recipe_bracer","cost":0,"secret_shop":0,"side_shop":0,"recipe":1,
               "localized_name":"Recipe"}],"status":200}}
            """).result;

            Assert.Equal(2250, result.items[0].cost);
            Assert.Equal("blink", result.items[0].shortName);
            Assert.False(result.items[0].isRecipe);
            Assert.True(result.items[1].isRecipe);
        }

        [Fact]
        public void GetRarities_deserializes()
        {
            Dota2RaritiesResult result = JsonSerializer.Deserialize<Dota2Rarities>("""
            {"result":{"rarities":[
              {"name":"immortal","id":6,"localized_name":"Immortal","color":"#e4ae33"}],"status":200}}
            """).result;

            Dota2Rarity rarity = Assert.Single(result.rarities);
            Assert.Equal(6, rarity.id);
            Assert.Equal("immortal", rarity.name);
            Assert.Equal("#e4ae33", rarity.color);
        }

        [Fact]
        public void GetLeagueListing_deserializes()
        {
            Dota2LeagueListingResult result = JsonSerializer.Deserialize<Dota2LeagueListing>("""
            {"result":{"leagues":[{"name":"The International","leagueid":16935,
            "description":"TI","tournament_url":"https://dota2.com","itemdef":12345}]}}
            """).result;

            Dota2League league = Assert.Single(result.leagues);
            Assert.Equal(16935, league.leagueid);
            Assert.Equal("The International", league.name);
            Assert.Equal(12345, league.itemdef);
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
