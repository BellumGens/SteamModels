# SteamModels-DotNet
[![Build Status](https://dev.azure.com/BellumGens/Bellum%20Gens/_apis/build/status/BellumGens.SteamModels?branchName=refs%2Fpull%2F5%2Fmerge)](https://dev.azure.com/BellumGens/Bellum%20Gens/_build/latest?definitionId=5&branchName=refs%2Fpull%2F5%2Fmerge)
[![.NET](https://github.com/BellumGens/SteamModels/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BellumGens/SteamModels/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/BellumGens/SteamModels/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/BellumGens/SteamModels/actions/workflows/codeql-analysis.yml)

.NET Models for the Steam API. Available on [NuGet](https://www.nuget.org/packages/SteamModels/).

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
