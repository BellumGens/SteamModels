nuget pack .\SteamModelsDotNetCore.csproj
nuget push .\SteamModelsDotNetCore.<version>.nupkg -Source https://www.nuget.org/api/v2/package

nuget push .\bin\Release\SteamModelsDotNetCore.1.0.0.nupkg -Source https://www.nuget.org/api/v2/package