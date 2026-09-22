# Changelog

All notable changes to this project are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project
adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

Releases before 10.0.0 predate this file. See the
[version history on NuGet](https://www.nuget.org/packages/SteamModels/#versions-body-tab) and the
git log for those.

## [Unreleased]

## [10.0.0] - 2026-09-22

Adds Dota 2 support and brings the existing models back in line with what the Steam API returns
today. The models in this release were checked against live Steam API responses rather than against
the documentation alone, which is what turned up most of the corrections below.

### Breaking

- **The library now targets .NET 10.** It previously targeted .NET 9, which left support in
  May 2026. Projects still on .NET 9 or earlier cannot consume 10.0.0.
- **`SteamUserGroup.membersOnLine` is now `SteamUserGroup.membersOnline`.** The profile xml spells
  the element `membersOnline`, so the old property never deserialized and always read 0. Code that
  referenced the old name will no longer compile, and code that read it was reading a constant 0.

### Added

#### Dota 2

A new `SteamModels.Dota2` namespace covering the Dota 2 interfaces that the Steam API still
exposes. Property names match the wire format, and each model adds read only helpers for the
fields Valve returns encoded.

| Model | Interface |
| --- | --- |
| `Dota2MatchHistory` | `IDOTA2Match_570/GetMatchHistory/v1` |
| `Dota2MatchDetails` | `IDOTA2Match_570/GetMatchDetails/v1` |
| `Dota2MatchHistoryBySequenceNum` | `IDOTA2Match_570/GetMatchHistoryBySequenceNum/v1` |
| `Dota2LiveLeagueGames` | `IDOTA2Match_570/GetLiveLeagueGames/v1` |
| `Dota2TopLiveGames` | `IDOTA2Match_570/GetTopLiveGame/v1` |
| `Dota2TeamInfo` | `IDOTA2Match_570/GetTeamInfoByTeamID/v1` |
| `Dota2Heroes` | `IEconDOTA2_570/GetHeroes/v1` |
| `Dota2Rarities` | `IEconDOTA2_570/GetRarities/v1` |
| `Dota2TournamentPrizePool` | `IEconDOTA2_570/GetTournamentPrizePool/v1` |

- `Dota2Ids`, converting between the 32 bit account id Dota 2 reports and the 64 bit steam id the
  rest of the Steam API uses, and decoding player slots into a team and a position.
- `Dota2PlayerSummary`, aggregating a set of matches for one account into win rate, kda, averages
  and a favourite hero, in the same spirit as `CSGOPlayerStats`.
- `Dota2Team`, `Dota2GameMode`, `Dota2LobbyType`, `Dota2LeaverStatus` and `Dota2SeriesType` enums,
  plus `[Flags]` enums `Dota2TowerStatus` and `Dota2BarracksStatus` for the building bit masks.

#### Steam

- `SteamUserSummary` gained the fields `GetPlayerSummaries` returns today: `realname`,
  `avatarhash`, `commentpermission`, `locstatecode`, `loccityid`, `gameid`, `gameextrainfo`,
  `gameserverip`, `gameserversteamid` and `lobbysteamid`.
- `SteamUser` gained `inGameInfo` (`SteamUserInGameInfo`), `inGameServerIP`, `weblinks`
  (`SteamUserWeblink`) and `steamRating`.
- `NewsItem.tags`.
- `PlayerStats.error` and `PlayerStats.success`, so a response that carries no stats can be told
  apart from one that does. An account that owns a game but has no stats for it comes back as an
  http 200 with a `steamID` and a `gameName` and no `stats` array at all, which `success` reports
  as `false`.

### Fixed

- `CSGOPlayerStats.killDeathRatio`, `headshotPercentage`, `accuracy`, `weapons` and
  `favouriteWeapon` threw `NullReferenceException` when the stats were absent, which is the normal
  response for a private profile. They now return 0, an empty list, or null.
- `CSGOPlayerStats.killDeathRatio`, `headshotPercentage` and `accuracy` threw
  `DivideByZeroException` when the stat they divide by was present with a value of 0. The existing
  fallbacks only covered a missing stat, not a zero one.
- `WeaponDescriptor.accuracy` threw `DivideByZeroException` for a weapon with no shots fired.
- The per weapon stat lookup used `SingleOrDefault`, which throws on a duplicate stat name. It now
  uses `FirstOrDefault`.
