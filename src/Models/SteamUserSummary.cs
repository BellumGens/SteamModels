using System.Collections.Generic;

namespace SteamModels
{
	/// <summary>
	/// Describing steam user summary obtained in bulk 
	/// GET: https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=<STEAM_API_KEY>&steamids=<COMMA_SEPARATED_STEAMID64_ARRAY>
	/// </summary>
	public class SteamUsersSummary
	{
		/// <summary>
		/// Gets or sets the response.
		/// </summary>
		/// <value>
		/// The response.
		/// </value>
		public PlayerResult response { get; set; }
	}

	public class PlayerResult
	{
		/// <summary>
		/// Gets or sets the players.
		/// </summary>
		/// <value>
		/// The players.
		/// </value>
		public List<SteamUserSummary> players { get; set; }
	}

	public class SteamUserSummary
	{
		/// <summary>
		/// Gets or sets the steamid.
		/// </summary>
		/// <value>
		/// The steamid.
		/// </value>
		public string steamid { get; set; }

		/// <summary>
		/// Gets or sets the communityvisibilitystate.
		/// </summary>
		/// <value>
		/// The communityvisibilitystate.
		/// </value>
		public int communityvisibilitystate { get; set; }

		/// <summary>
		/// Gets or sets the profilestate.
		/// </summary>
		/// <value>
		/// The profilestate.
		/// </value>
		public int profilestate { get; set; }

		/// <summary>
		/// Gets or sets the personaname.
		/// </summary>
		/// <value>
		/// The personaname.
		/// </value>
		public string personaname { get; set; }

		/// <summary>
		/// Gets or sets the lastlogoff.
		/// </summary>
		/// <value>
		/// The lastlogoff.
		/// </value>
		public int lastlogoff { get; set; }

		/// <summary>
		/// Gets or sets the profileurl.
		/// </summary>
		/// <value>
		/// The profileurl.
		/// </value>
		public string profileurl { get; set; }

		/// <summary>
		/// Gets or sets the avatar.
		/// </summary>
		/// <value>
		/// The avatar.
		/// </value>
		public string avatar { get; set; }

		/// <summary>
		/// Gets or sets the avatarmedium.
		/// </summary>
		/// <value>
		/// The avatarmedium.
		/// </value>
		public string avatarmedium { get; set; }

		/// <summary>
		/// Gets or sets the avatarfull.
		/// </summary>
		/// <value>
		/// The avatarfull.
		/// </value>
		public string avatarfull { get; set; }

		/// <summary>
		/// Gets or sets the personastate.
		/// </summary>
		/// <value>
		/// The personastate.
		/// </value>
		public int personastate { get; set; }

		/// <summary>
		/// Gets or sets the primaryclanid.
		/// </summary>
		/// <value>
		/// The primaryclanid.
		/// </value>
		public string primaryclanid { get; set; }

		/// <summary>
		/// Gets or sets the timecreated.
		/// </summary>
		/// <value>
		/// The timecreated.
		/// </value>
		public int timecreated { get; set; }

		/// <summary>
		/// Gets or sets the personastateflags.
		/// </summary>
		/// <value>
		/// The personastateflags.
		/// </value>
		public int personastateflags { get; set; }

		/// <summary>
		/// Gets or sets the loccountrycode.
		/// </summary>
		/// <value>
		/// The loccountrycode.
		/// </value>
		public string loccountrycode { get; set; }

		/// <summary>
		/// Gets or sets the locstatecode.
		/// </summary>
		/// <value>
		/// The locstatecode.
		/// </value>
		public string locstatecode { get; set; }

		/// <summary>
		/// Gets or sets the loccityid.
		/// </summary>
		/// <value>
		/// The loccityid.
		/// </value>
		public int loccityid { get; set; }

		/// <summary>
		/// Gets or sets the realname. Only returned when the profile is public.
		/// </summary>
		/// <value>
		/// The realname.
		/// </value>
		public string realname { get; set; }

		/// <summary>
		/// Gets or sets the commentpermission. Present when the profile allows public comments.
		/// </summary>
		/// <value>
		/// The commentpermission.
		/// </value>
		public int commentpermission { get; set; }

		/// <summary>
		/// Gets or sets the avatarhash, the SHA1 hash the avatar urls are built from.
		/// </summary>
		/// <value>
		/// The avatarhash.
		/// </value>
		public string avatarhash { get; set; }

		/// <summary>
		/// Gets or sets the appid of the game the user is currently playing.
		/// Returned as a string by the Steam API and only present while the user is in game.
		/// </summary>
		/// <value>
		/// The gameid.
		/// </value>
		public string gameid { get; set; }

		/// <summary>
		/// Gets or sets the name of the game the user is currently playing.
		/// Only present while the user is in game.
		/// </summary>
		/// <value>
		/// The gameextrainfo.
		/// </value>
		public string gameextrainfo { get; set; }

		/// <summary>
		/// Gets or sets the ip of the game server the user is currently playing on.
		/// Only present while the user is on a game server.
		/// </summary>
		/// <value>
		/// The gameserverip.
		/// </value>
		public string gameserverip { get; set; }

		/// <summary>
		/// Gets or sets the steam id of the game server the user is currently playing on.
		/// Only present while the user is on a game server.
		/// </summary>
		/// <value>
		/// The gameserversteamid.
		/// </value>
		public string gameserversteamid { get; set; }

		/// <summary>
		/// Gets or sets the steam id of the lobby the user is currently in.
		/// Only present while the user is in a lobby.
		/// </summary>
		/// <value>
		/// The lobbysteamid.
		/// </value>
		public string lobbysteamid { get; set; }
	}
}
