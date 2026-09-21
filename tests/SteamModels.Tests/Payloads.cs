namespace SteamModels.Tests
{
    /// <summary>
    /// Sample Steam API payloads shared across the test suites. They are trimmed down but keep the
    /// shape, the field names and the quirks of what the Steam API actually returns.
    /// </summary>
    internal static class Payloads
    {
        /// <summary>
        /// The body of a Dota 2 match, i.e. what GetMatchDetails returns under its result property
        /// and what GetMatchHistoryBySequenceNum returns for each entry of its matches array.
        /// The Radiant wins, the first player is a Radiant core that stayed for the whole match and
        /// the second is an anonymous Dire player who abandoned.
        /// </summary>
        public const string MatchBody = """
        {
          "players":[
            {"account_id":86745912,"player_slot":0,"team_number":0,"team_slot":0,"hero_id":8,"hero_variant":2,
             "item_0":108,"item_1":116,"item_2":0,"item_3":0,"item_4":0,"item_5":0,
             "backpack_0":0,"backpack_1":0,"backpack_2":0,"item_neutral":289,"item_neutral2":379,
             "kills":12,"deaths":4,"assists":6,"leaver_status":0,"last_hits":250,"denies":12,
             "gold_per_min":650,"xp_per_min":720,"level":25,"net_worth":24500,
             "aghanims_scepter":1,"aghanims_shard":1,"moonshard":0,
             "hero_damage":32000,"tower_damage":8000,"hero_healing":0,"gold":1200,"gold_spent":23000,
             "scaled_hero_damage":21000,"scaled_tower_damage":7000,"scaled_hero_healing":0,
             "ability_upgrades":[{"ability":5003,"time":141,"level":1}],
             "additional_units":[{"unitname":"npc_dota_lone_druid_bear","item_0":63,"item_1":0,"item_2":0,
                                  "item_3":0,"item_4":0,"item_5":0,"backpack_0":0,"backpack_1":0,
                                  "backpack_2":0,"item_neutral":0}]},
            {"account_id":4294967295,"player_slot":128,"team_number":1,"team_slot":0,"hero_id":14,
             "kills":3,"deaths":9,"assists":2,"leaver_status":3,"last_hits":90,"denies":2,
             "gold_per_min":300,"xp_per_min":350,"level":16,"net_worth":9000}
          ],
          "radiant_win":true,"duration":2251,"pre_game_duration":90,"start_time":1446069903,
          "match_id":1900000000,"match_seq_num":1700000000,
          "tower_status_radiant":2047,"tower_status_dire":1974,
          "barracks_status_radiant":63,"barracks_status_dire":51,
          "cluster":111,"first_blood_time":25,"lobby_type":7,"human_players":10,"leagueid":0,
          "positive_votes":0,"negative_votes":0,"game_mode":22,"flags":1,"engine":1,
          "radiant_score":30,"dire_score":20,
          "picks_bans":[{"is_pick":false,"hero_id":41,"team":0,"order":0},
                        {"is_pick":true,"hero_id":8,"team":0,"order":6}],
          "radiant_team_id":2163,"radiant_name":"Team Secret","radiant_logo":892329808893452930,
          "radiant_team_complete":1,
          "dire_team_id":39,"dire_name":"Evil Geniuses","dire_logo":892329808893452931,
          "dire_team_complete":1,
          "radiant_captain":86745912,"dire_captain":70388657
        }
        """;

        /// <summary>
        /// A Dota 2 GetMatchDetails response.
        /// </summary>
        public const string MatchDetails = """{"result":""" + MatchBody + "}";

        /// <summary>
        /// A Dota 2 GetMatchHistoryBySequenceNum response, which returns full match bodies.
        /// </summary>
        public const string MatchHistoryBySequenceNum =
            """{"result":{"status":1,"matches":[""" + MatchBody + "]}}";

        /// <summary>
        /// The response GetMatchDetails returns for a match id that does not exist.
        /// </summary>
        public const string MatchDetailsError = """{"result":{"error":"Match ID not found"}}""";

        /// <summary>
        /// The account id of the Radiant player in <see cref="MatchBody"/>.
        /// </summary>
        public const uint AccountId = 86745912;

        /// <summary>
        /// The 64 bit steam id matching <see cref="AccountId"/>.
        /// </summary>
        public const long SteamId64 = 76561198047011640L;
    }
}
