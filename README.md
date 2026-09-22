# SteamModels-DotNet
[![Publish](https://github.com/BellumGens/SteamModels/actions/workflows/publish.yml/badge.svg)](https://github.com/BellumGens/SteamModels/actions/workflows/publish.yml)
[![.NET](https://github.com/BellumGens/SteamModels/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BellumGens/SteamModels/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/BellumGens/SteamModels/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/BellumGens/SteamModels/actions/workflows/codeql-analysis.yml)
[![NuGet](https://img.shields.io/nuget/v/SteamModels)](https://www.nuget.org/packages/SteamModels/)

.NET Models for the Steam API, with game specific models for Counter-Strike (`SteamModels.CSGO`) and Dota 2 (`SteamModels.Dota2`).

# Usage
The Steam API results can be deserialized into the .NET model classes provided by SteamModels.  

### Example with `SteamUser`:

```C#
public class SteamService
{
    private readonly string _playerDetailsUrl = "http://steamcommunity.com/id/{0}/?xml=1";

    private string _username;

    public SteamUser GetSteamUserDetails(string name)
    {
        HttpClient client = new HttpClient();
        this._username = NormalizeUsername(name);
        var playerDetailsResponse = client.GetStreamAsync(this._username);
        XmlSerializer serializer = new XmlSerializer(typeof(SteamUser));
        SteamUser user = (SteamUser)serializer.Deserialize(playerDetailsResponse.Result);
        return user;
    }

    public string NormalizeUsername(string name)
    {
        return name.Contains("http://") ? name : string.Format(_playerDetailsUrl, name);
    }
}
```

### Example with `SteamNews`:

```C#
public class SteamService
{
    private readonly string _steamAppNewsUrl = "http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid={0}&maxlength=300&format=json";

    public async Task<SteamNews> GetSteamAppNewsJSON(int appid)
    {
	HttpClient client = new HttpClient();
	var steamnews = await client.GetStringAsync(string.format(this._steamAppNewsUrl, appid));
	SteamNews news = JsonConvert.DeserializeObject<SteamNews>(steamnews);
	return news;
    }
}
```

### Example with `SteamUserStats`:

```C#
public class SteamService
{
    private readonly string _steamUserStatsUrl = "http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={0}&key={1}&steamid={2}&format=json";

    public static SteamUserStats GetStatsForGame(string username)
    {
        HttpClient client = new HttpClient();
        var statsForGameResponse = client.GetStringAsync(string.Format(_steamUserStatsUrl, SteamInfo.Config.gameId, SteamInfo.Config.steamApiKey, username));
        SteamUserStats statsForUser = JsonConvert.DeserializeObject<SteamUserStats>(statsForGameResponse.Result);
        return statsForUser;
    }
}
```

### Example with `CSGOPlayerStats`, which is a superset of `SteamUserStats` with specific parsing for some CS:GO stats, like HS%, Accuracy, Top Weapon, Total Kills, etc:

```C#
public class SteamService
{
    private readonly string _steamUserStatsUrl = "http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={0}&key={1}&steamid={2}&format=json";

    public static CSGOPlayerStats GetStatsForGame(string username)
    {
        HttpClient client = new HttpClient();
        var statsForGameResponse = client.GetStringAsync(string.Format(_steamUserStatsUrl, 730, SteamInfo.Config.steamApiKey, username));
        CSGOPlayerStats statsForUser = JsonConvert.DeserializeObject<CSGOPlayerStats>(statsForGameResponse.Result);
        return statsForUser;
    }
}
```

### Example with `SteamGroup`

```C#
private static readonly string _groupMembersUrl = "https://steamcommunity.com/gid/{0}/memberslistxml/?xml=1";

public static SteamGroup GetSteamGroup(string groupid)
{
    HttpClient client = new HttpClient();
    var playerDetailsResponse = client.GetStreamAsync(string.Format(_groupMembersUrl, groupid));
    XmlSerializer serializer = new XmlSerializer(typeof(SteamGroup));
    SteamGroup group = (SteamGroup)serializer.Deserialize(playerDetailsResponse.Result);
    return group;
}
```

# Dota 2

The `SteamModels.Dota2` namespace maps the Dota 2 Steam API interfaces. Property names match the
Steam API payloads exactly, so the models deserialize without any converters, and each model adds
read only helpers for the fields Valve returns encoded, such as player slots, tower bit masks and
unix timestamps.

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

### Example with `Dota2MatchDetails`:

```C#
public class Dota2Service
{
    private readonly string _matchDetailsUrl = "https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/v1/?key={0}&match_id={1}";

    public async Task<Dota2MatchDetailsResult> GetMatchDetails(long matchId)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetStringAsync(string.Format(_matchDetailsUrl, SteamInfo.Config.steamApiKey, matchId));
        Dota2MatchDetails match = JsonSerializer.Deserialize<Dota2MatchDetails>(response);
        return match.result;
    }
}
```

`Dota2MatchDetailsResult` parses the encoded fields for you:

```C#
Dota2MatchDetailsResult match = await GetMatchDetails(8_000_000_000);

if (!match.success)
{
    // match.error holds what the Steam API returned, e.g. "Match ID not found"
    return;
}

Console.WriteLine(match.winner);                // Radiant
Console.WriteLine(match.gameMode);              // AllDraft
Console.WriteLine(match.lobbyType);             // Ranked
Console.WriteLine(match.durationTime);          // 00:37:31
Console.WriteLine(match.startTime);             // DateTimeOffset
Console.WriteLine(match.direTowerStatus);       // TopTier1, TopTier2, MiddleTier2, ...

Dota2MatchPlayer player = match.GetPlayer(86745912);
Console.WriteLine(player.team);                 // Radiant
Console.WriteLine(player.slot);                 // 0 through 4
Console.WriteLine(player.kda);                  // 4.50
Console.WriteLine(player.leaverStatus);         // None
Console.WriteLine(match.DidWin(player));        // true
```

### Converting between account ids and steam ids

Dota 2 reports players by their 32 bit account id while the rest of the Steam API uses the 64 bit
steam id. `Dota2Ids` converts between the two, so Dota 2 results can be joined with `SteamUser` or
`SteamUserSummary` results:

```C#
long steamId64 = Dota2Ids.ToSteamId64(86745912);        // 76561198047011640
uint accountId = Dota2Ids.ToAccountId(76561198047011640);

// players who have hidden their match history come back as Dota2Ids.AnonymousAccountId,
// and the steamId64 helper on the player models returns null for them
long? id = player.steamId64;
```

### Example with `Dota2PlayerSummary`

Dota 2 exposes almost nothing through the generic `ISteamUserStats` interface, so a player profile
has to be built from match details. `Dota2PlayerSummary` aggregates a set of matches for one player,
in the same spirit as `CSGOPlayerStats`:

```C#
IEnumerable<Dota2MatchDetailsResult> matches = await GetRecentMatches(accountId);
Dota2PlayerSummary summary = new Dota2PlayerSummary(accountId, matches);

Console.WriteLine(summary.matches);                     // 3
Console.WriteLine(summary.winRate);                     // 66.67
Console.WriteLine(summary.kda);                         // 4.50
Console.WriteLine(summary.averageGoldPerMinute);        // 650
Console.WriteLine(summary.abandons);                    // 0
Console.WriteLine(summary.favouriteHero.heroId);        // 8
Console.WriteLine(summary.favouriteHero.winRate);       // 100
```

Matches the player did not play in, and matches that came back as an error, are skipped, and every
derived value returns 0 rather than throwing when there is nothing to divide by.

### A note on serializers

The models carry no serializer dependency of their own, so either `System.Text.Json` or
`Newtonsoft.Json` will deserialize them. The read only helper properties are annotated with
`System.Text.Json.Serialization.JsonIgnore`, which keeps them out of the payload when a model is
serialized back out with `System.Text.Json`. `Newtonsoft.Json` does not honour that attribute and
will include them.

# Building and testing

The library targets .NET 10.

```
dotnet build
dotnet test
```

The test suite covers the Steam API payload shapes the models map, including the quirks that are
easy to regress: the error payloads returned for private profiles and unknown matches, the encoded
player slot and building bit masks, the string ids and scientific notation timestamp that
`GetTopLiveGame` returns, and the live Dota 2 scoreboard field names, which differ from the ones
used by match details.

## Releasing

Releases go out through GitHub Actions. Publishing a GitHub Release runs
[`publish.yml`](.github/workflows/publish.yml), which builds, runs the tests, packs and pushes to
NuGet.org.

Record what changed in [`CHANGELOG.md`](CHANGELOG.md) before you cut the release, so the release
notes and the changelog agree.

The release tag is the version. Tag a release `v10.1.0`, and that is the version the package, the
assembly and the file version all carry, so there is no version to bump in the csproj. The
`<Version>` in `src/SteamModels.csproj` is only the local default for `dotnet build` and
`dotnet pack` runs on your machine. Prereleases work the same way, tag them `v10.1.0-beta.1`.
A tag that is not a version fails the workflow before anything is published.
