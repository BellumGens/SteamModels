using System;

namespace SteamModels.Dota2
{
    /// <summary>
    /// The team a player, pick or ban belongs to.
    /// </summary>
    public enum Dota2Team
    {
        /// <summary>
        /// The Radiant team.
        /// </summary>
        Radiant = 0,

        /// <summary>
        /// The Dire team.
        /// </summary>
        Dire = 1
    }

    /// <summary>
    /// The game mode a match was played in, as reported by the game_mode field.
    /// </summary>
    public enum Dota2GameMode
    {
        /// <summary>
        /// Unknown or not reported.
        /// </summary>
        None = 0,

        /// <summary>
        /// All Pick.
        /// </summary>
        AllPick = 1,

        /// <summary>
        /// Captains Mode.
        /// </summary>
        CaptainsMode = 2,

        /// <summary>
        /// Random Draft.
        /// </summary>
        RandomDraft = 3,

        /// <summary>
        /// Single Draft.
        /// </summary>
        SingleDraft = 4,

        /// <summary>
        /// All Random.
        /// </summary>
        AllRandom = 5,

        /// <summary>
        /// Intro.
        /// </summary>
        Intro = 6,

        /// <summary>
        /// The Diretide event mode.
        /// </summary>
        Diretide = 7,

        /// <summary>
        /// Reverse Captains Mode.
        /// </summary>
        ReverseCaptainsMode = 8,

        /// <summary>
        /// The Greeviling event mode.
        /// </summary>
        TheGreeviling = 9,

        /// <summary>
        /// Tutorial.
        /// </summary>
        Tutorial = 10,

        /// <summary>
        /// Mid Only.
        /// </summary>
        MidOnly = 11,

        /// <summary>
        /// Least Played.
        /// </summary>
        LeastPlayed = 12,

        /// <summary>
        /// Limited Heroes, also known as the new player pool.
        /// </summary>
        LimitedHeroes = 13,

        /// <summary>
        /// Compendium matchmaking.
        /// </summary>
        CompendiumMatchmaking = 14,

        /// <summary>
        /// Custom game.
        /// </summary>
        Custom = 15,

        /// <summary>
        /// Captains Draft.
        /// </summary>
        CaptainsDraft = 16,

        /// <summary>
        /// Balanced Draft.
        /// </summary>
        BalancedDraft = 17,

        /// <summary>
        /// Ability Draft.
        /// </summary>
        AbilityDraft = 18,

        /// <summary>
        /// Event mode.
        /// </summary>
        Event = 19,

        /// <summary>
        /// All Random Deathmatch.
        /// </summary>
        AllRandomDeathmatch = 20,

        /// <summary>
        /// 1v1 mid.
        /// </summary>
        OneVersusOneMid = 21,

        /// <summary>
        /// All Draft, the mode ranked all pick reports itself as.
        /// </summary>
        AllDraft = 22,

        /// <summary>
        /// Turbo.
        /// </summary>
        Turbo = 23,

        /// <summary>
        /// Mutation.
        /// </summary>
        Mutation = 24
    }

    /// <summary>
    /// The lobby a match was played in, as reported by the lobby_type field.
    /// </summary>
    public enum Dota2LobbyType
    {
        /// <summary>
        /// Invalid lobby.
        /// </summary>
        Invalid = -1,

        /// <summary>
        /// Public matchmaking.
        /// </summary>
        PublicMatchmaking = 0,

        /// <summary>
        /// Practice lobby, which also covers most custom and private lobbies.
        /// </summary>
        Practice = 1,

        /// <summary>
        /// Tournament lobby.
        /// </summary>
        Tournament = 2,

        /// <summary>
        /// Tutorial lobby.
        /// </summary>
        Tutorial = 3,

        /// <summary>
        /// Co-op with bots.
        /// </summary>
        CoopWithBots = 4,

        /// <summary>
        /// Team match.
        /// </summary>
        TeamMatch = 5,

        /// <summary>
        /// Solo queue.
        /// </summary>
        SoloQueue = 6,

        /// <summary>
        /// Ranked matchmaking.
        /// </summary>
        Ranked = 7,

        /// <summary>
        /// Solo mid 1v1.
        /// </summary>
        SoloMid = 8,

        /// <summary>
        /// Battle cup.
        /// </summary>
        BattleCup = 9
    }

    /// <summary>
    /// Whether and how a player left a match, as reported by the leaver_status field.
    /// </summary>
    public enum Dota2LeaverStatus
    {
        /// <summary>
        /// The player stayed for the whole match.
        /// </summary>
        None = 0,

        /// <summary>
        /// The player disconnected, but not for long enough to count as an abandon.
        /// </summary>
        Disconnected = 1,

        /// <summary>
        /// The player disconnected for too long and abandoned the match.
        /// </summary>
        DisconnectedTooLong = 2,

        /// <summary>
        /// The player abandoned the match.
        /// </summary>
        Abandoned = 3,

        /// <summary>
        /// The player was away from keyboard.
        /// </summary>
        Afk = 4,

        /// <summary>
        /// The player never connected to the match.
        /// </summary>
        NeverConnected = 5,

        /// <summary>
        /// The player never connected and the match waited too long for them.
        /// </summary>
        NeverConnectedTooLong = 6
    }

    /// <summary>
    /// The format of a league series, as reported by the series_type field.
    /// </summary>
    public enum Dota2SeriesType
    {
        /// <summary>
        /// A single game rather than a series.
        /// </summary>
        NonSeries = 0,

        /// <summary>
        /// A best of three series.
        /// </summary>
        BestOfThree = 1,

        /// <summary>
        /// A best of five series.
        /// </summary>
        BestOfFive = 2
    }

    /// <summary>
    /// The towers a team still has standing, as reported by the tower_status_radiant,
    /// tower_status_dire and tower_state fields.
    /// A set bit means the tower is still standing.
    /// </summary>
    [Flags]
    public enum Dota2TowerStatus
    {
        /// <summary>
        /// No towers standing.
        /// </summary>
        None = 0,

        /// <summary>
        /// The tier 1 tower on the top lane.
        /// </summary>
        TopTier1 = 1 << 0,

        /// <summary>
        /// The tier 2 tower on the top lane.
        /// </summary>
        TopTier2 = 1 << 1,

        /// <summary>
        /// The tier 3 tower on the top lane.
        /// </summary>
        TopTier3 = 1 << 2,

        /// <summary>
        /// The tier 1 tower on the middle lane.
        /// </summary>
        MiddleTier1 = 1 << 3,

        /// <summary>
        /// The tier 2 tower on the middle lane.
        /// </summary>
        MiddleTier2 = 1 << 4,

        /// <summary>
        /// The tier 3 tower on the middle lane.
        /// </summary>
        MiddleTier3 = 1 << 5,

        /// <summary>
        /// The tier 1 tower on the bottom lane.
        /// </summary>
        BottomTier1 = 1 << 6,

        /// <summary>
        /// The tier 2 tower on the bottom lane.
        /// </summary>
        BottomTier2 = 1 << 7,

        /// <summary>
        /// The tier 3 tower on the bottom lane.
        /// </summary>
        BottomTier3 = 1 << 8,

        /// <summary>
        /// The tier 4 tower closest to the top of the ancient.
        /// </summary>
        AncientTop = 1 << 9,

        /// <summary>
        /// The tier 4 tower closest to the bottom of the ancient.
        /// </summary>
        AncientBottom = 1 << 10
    }

    /// <summary>
    /// The barracks a team still has standing, as reported by the barracks_status_radiant,
    /// barracks_status_dire and barracks_state fields.
    /// A set bit means the barracks is still standing.
    /// </summary>
    [Flags]
    public enum Dota2BarracksStatus
    {
        /// <summary>
        /// No barracks standing.
        /// </summary>
        None = 0,

        /// <summary>
        /// The ranged barracks on the top lane.
        /// </summary>
        TopRanged = 1 << 0,

        /// <summary>
        /// The melee barracks on the top lane.
        /// </summary>
        TopMelee = 1 << 1,

        /// <summary>
        /// The ranged barracks on the middle lane.
        /// </summary>
        MiddleRanged = 1 << 2,

        /// <summary>
        /// The melee barracks on the middle lane.
        /// </summary>
        MiddleMelee = 1 << 3,

        /// <summary>
        /// The ranged barracks on the bottom lane.
        /// </summary>
        BottomRanged = 1 << 4,

        /// <summary>
        /// The melee barracks on the bottom lane.
        /// </summary>
        BottomMelee = 1 << 5
    }
}
