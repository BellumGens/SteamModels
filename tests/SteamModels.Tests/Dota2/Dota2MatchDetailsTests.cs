using System.Text.Json;
using SteamModels.Dota2;

namespace SteamModels.Tests.Dota2
{
    public class Dota2MatchDetailsTests
    {
        private static Dota2MatchDetailsResult Match =>
            JsonSerializer.Deserialize<Dota2MatchDetails>(Payloads.MatchDetails).result;

        [Fact]
        public void Deserializes_the_match_wide_fields()
        {
            Dota2MatchDetailsResult match = Match;

            Assert.True(match.success);
            Assert.Equal(1900000000L, match.match_id);
            Assert.Equal(1700000000L, match.match_seq_num);
            Assert.Equal(2251, match.duration);
            Assert.Equal(90, match.pre_game_duration);
            Assert.Equal(30, match.radiant_score);
            Assert.Equal(20, match.dire_score);
            Assert.Equal(10, match.human_players);
            Assert.Equal(25, match.first_blood_time);
            Assert.Equal(111, match.cluster);
        }

        [Fact]
        public void Deserializes_the_team_fields()
        {
            Dota2MatchDetailsResult match = Match;

            Assert.Equal(2163u, match.radiant_team_id);
            Assert.Equal("Team Secret", match.radiant_name);
            Assert.Equal(892329808893452930UL, match.radiant_logo);
            Assert.Equal(1, match.radiant_team_complete);
            Assert.Equal("Evil Geniuses", match.dire_name);
            Assert.Equal(Payloads.AccountId, match.radiant_captain);
        }

        [Fact]
        public void Parses_the_encoded_match_fields()
        {
            Dota2MatchDetailsResult match = Match;

            Assert.Equal(Dota2Team.Radiant, match.winner);
            Assert.Equal(Dota2GameMode.AllDraft, match.gameMode);
            Assert.Equal(Dota2LobbyType.Ranked, match.lobbyType);
            Assert.Equal(TimeSpan.FromSeconds(2251), match.durationTime);
            Assert.Equal(1446069903L, match.startTime.ToUnixTimeSeconds());
        }

        [Fact]
        public void Tower_status_decodes_into_the_towers_left_standing()
        {
            Dota2MatchDetailsResult match = Match;

            // 2047 is every bit of the 11 tower mask set
            Assert.Equal((Dota2TowerStatus)2047, match.radiantTowerStatus);
            Assert.True(match.radiantTowerStatus.HasFlag(Dota2TowerStatus.AncientTop));
            Assert.True(match.radiantTowerStatus.HasFlag(Dota2TowerStatus.AncientBottom));

            // 1974 is every tier 1 tower destroyed and everything else still standing
            Assert.False(match.direTowerStatus.HasFlag(Dota2TowerStatus.TopTier1));
            Assert.False(match.direTowerStatus.HasFlag(Dota2TowerStatus.MiddleTier1));
            Assert.False(match.direTowerStatus.HasFlag(Dota2TowerStatus.BottomTier1));
            Assert.True(match.direTowerStatus.HasFlag(Dota2TowerStatus.TopTier2));
            Assert.True(match.direTowerStatus.HasFlag(Dota2TowerStatus.BottomTier3));
            Assert.True(match.direTowerStatus.HasFlag(Dota2TowerStatus.AncientTop));
        }

        [Fact]
        public void Barracks_status_decodes_into_the_barracks_left_standing()
        {
            Dota2MatchDetailsResult match = Match;

            Assert.Equal((Dota2BarracksStatus)63, match.radiantBarracksStatus);

            // 51 leaves the middle barracks razed
            Assert.False(match.direBarracksStatus.HasFlag(Dota2BarracksStatus.MiddleMelee));
            Assert.False(match.direBarracksStatus.HasFlag(Dota2BarracksStatus.MiddleRanged));
            Assert.True(match.direBarracksStatus.HasFlag(Dota2BarracksStatus.TopMelee));
            Assert.True(match.direBarracksStatus.HasFlag(Dota2BarracksStatus.BottomMelee));
        }

        [Fact]
        public void Deserializes_the_current_patch_player_fields()
        {
            Dota2MatchPlayer player = Match.GetPlayer(Payloads.AccountId);

            Assert.Equal(2, player.hero_variant);
            Assert.Equal(24500, player.net_worth);
            Assert.Equal(1, player.aghanims_scepter);
            Assert.Equal(1, player.aghanims_shard);
            Assert.Equal(0, player.moonshard);
            Assert.Equal(0, player.team_number);
            Assert.Equal(0, player.team_slot);
            Assert.Equal(289, player.item_neutral);
            Assert.Equal(379, player.item_neutral2);
        }

        [Fact]
        public void Parses_the_encoded_player_fields()
        {
            Dota2MatchDetailsResult match = Match;
            Dota2MatchPlayer player = match.GetPlayer(Payloads.AccountId);

            Assert.Equal(Dota2Team.Radiant, player.team);
            Assert.Equal(0, player.slot);
            Assert.Equal(Payloads.SteamId64, player.steamId64);
            Assert.Equal(Dota2LeaverStatus.None, player.leaverStatus);
            Assert.False(player.abandoned);
            Assert.True(match.DidWin(player));
        }

        [Fact]
        public void Kda_counts_kills_and_assists_per_death()
        {
            // 12 kills, 6 assists, 4 deaths
            Assert.Equal(4.50m, Match.GetPlayer(Payloads.AccountId).kda);
        }

        [Fact]
        public void Kda_treats_a_death_free_match_as_one_death()
        {
            Dota2MatchPlayer player = new Dota2MatchPlayer { kills = 5, assists = 3, deaths = 0 };

            Assert.Equal(8m, player.kda);
        }

        [Fact]
        public void Inventory_slots_are_exposed_in_order()
        {
            Dota2MatchPlayer player = Match.GetPlayer(Payloads.AccountId);

            Assert.Equal(new[] { 108, 116, 0, 0, 0, 0 }, player.items);
            Assert.Equal(new[] { 0, 0, 0 }, player.backpack);
        }

        [Fact]
        public void Deserializes_ability_upgrades_and_additional_units()
        {
            Dota2MatchPlayer player = Match.GetPlayer(Payloads.AccountId);

            Dota2AbilityUpgrade upgrade = Assert.Single(player.ability_upgrades);
            Assert.Equal(5003, upgrade.ability);
            Assert.Equal(141, upgrade.time);
            Assert.Equal(1, upgrade.level);

            Dota2AdditionalUnit unit = Assert.Single(player.additional_units);
            Assert.Equal("npc_dota_lone_druid_bear", unit.unitname);
            Assert.Equal(63, unit.items[0]);
            Assert.Equal(new[] { 0, 0, 0 }, unit.backpack);
        }

        [Fact]
        public void An_anonymous_player_has_no_steam_id()
        {
            Dota2MatchPlayer player = Match.GetPlayer(Dota2Ids.AnonymousAccountId);

            Assert.NotNull(player);
            Assert.Null(player.steamId64);
        }

        [Fact]
        public void A_losing_abandoning_player_is_reported_as_such()
        {
            Dota2MatchDetailsResult match = Match;
            Dota2MatchPlayer player = match.GetPlayer(Dota2Ids.AnonymousAccountId);

            Assert.Equal(Dota2Team.Dire, player.team);
            Assert.Equal(Dota2LeaverStatus.Abandoned, player.leaverStatus);
            Assert.True(player.abandoned);
            Assert.False(match.DidWin(player));
        }

        [Theory]
        [InlineData(Dota2LeaverStatus.None, false)]
        [InlineData(Dota2LeaverStatus.Disconnected, false)]
        [InlineData(Dota2LeaverStatus.DisconnectedTooLong, true)]
        [InlineData(Dota2LeaverStatus.Abandoned, true)]
        [InlineData(Dota2LeaverStatus.Afk, true)]
        public void A_short_disconnect_is_not_an_abandon(Dota2LeaverStatus status, bool abandoned)
        {
            Dota2MatchPlayer player = new Dota2MatchPlayer { leaver_status = (int)status };

            Assert.Equal(abandoned, player.abandoned);
        }

        [Fact]
        public void Deserializes_picks_and_bans()
        {
            List<Dota2PickBan> draft = Match.picks_bans;

            Assert.Equal(2, draft.Count);
            Assert.False(draft[0].is_pick);
            Assert.Equal(41, draft[0].hero_id);
            Assert.Equal(Dota2Team.Radiant, draft[0].pickingTeam);
            Assert.True(draft[1].is_pick);
            Assert.Equal(6, draft[1].order);
        }

        [Fact]
        public void GetPlayer_returns_null_for_someone_who_was_not_in_the_match()
        {
            Assert.Null(Match.GetPlayer(1));
        }

        [Fact]
        public void DidWin_rejects_a_null_player()
        {
            Assert.Throws<ArgumentNullException>(() => Match.DidWin(null));
        }

        [Fact]
        public void An_error_response_is_surfaced_rather_than_looking_like_an_empty_match()
        {
            Dota2MatchDetailsResult match =
                JsonSerializer.Deserialize<Dota2MatchDetails>(Payloads.MatchDetailsError).result;

            Assert.False(match.success);
            Assert.Equal("Match ID not found", match.error);
            Assert.Null(match.GetPlayer(Payloads.AccountId));
        }

        [Fact]
        public void GetMatchHistoryBySequenceNum_returns_full_match_details()
        {
            Dota2MatchHistoryBySequenceNumResult result =
                JsonSerializer.Deserialize<Dota2MatchHistoryBySequenceNum>(Payloads.MatchHistoryBySequenceNum).result;

            Assert.Equal(1, result.status);
            Dota2MatchDetailsResult match = Assert.Single(result.matches);
            Assert.Equal(1900000000L, match.match_id);
            Assert.Equal(Dota2Team.Radiant, match.winner);
            Assert.Equal(2, match.players.Count);
        }
    }
}
