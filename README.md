# SteamModels-DotNet
.NET Models for the Steam API. Available on [NuGet](https://www.nuget.org/packages/SteamModels/).

# Usage
The Steam API results can be deserialized into the .NET model classes provided by SteamModels.  

Example with `SteamUser`:

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

Example with `SteamNews`:

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
