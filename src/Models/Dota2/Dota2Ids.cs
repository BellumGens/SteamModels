using System;

namespace SteamModels.Dota2
{
    /// <summary>
    /// Constants and identifier helpers for the Dota 2 Steam API interfaces.
    /// Dota 2 identifies players by their 32 bit account id while the rest of the Steam API uses
    /// the 64 bit steam id, so converting between the two is needed to join Dota 2 results with
    /// <see cref="SteamUser"/> or <see cref="SteamUserSummary"/> results.
    /// </summary>
    public static class Dota2Ids
    {
        /// <summary>
        /// The Dota 2 steam application id.
        /// </summary>
        public const int AppId = 570;

        /// <summary>
        /// The account id reported for a player who has hidden their match history.
        /// </summary>
        public const uint AnonymousAccountId = uint.MaxValue;

        /// <summary>
        /// The offset between a 32 bit account id and the 64 bit steam id of an individual account.
        /// </summary>
        public const long SteamId64Base = 76561197960265728L;

        /// <summary>
        /// The bit of a player slot that holds the team the player belongs to.
        /// </summary>
        private const int TeamBit = 0x80;

        /// <summary>
        /// The bits of a player slot that hold the position of the player within their team.
        /// </summary>
        private const int SlotBits = 0x07;

        /// <summary>
        /// Converts a Dota 2 account id to the matching 64 bit steam id.
        /// </summary>
        /// <param name="accountId">The 32 bit account id.</param>
        /// <returns>The 64 bit steam id.</returns>
        public static long ToSteamId64(uint accountId)
        {
            return SteamId64Base + accountId;
        }

        /// <summary>
        /// Converts a 64 bit steam id to the matching Dota 2 account id.
        /// </summary>
        /// <param name="steamId64">The 64 bit steam id.</param>
        /// <returns>The 32 bit account id.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="steamId64"/> is not a valid individual steam id.
        /// </exception>
        public static uint ToAccountId(long steamId64)
        {
            long accountId = steamId64 - SteamId64Base;
            if (accountId < 0 || accountId > uint.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(steamId64), steamId64, "Not an individual steam id.");
            }
            return (uint)accountId;
        }

        /// <summary>
        /// Converts a 64 bit steam id in its string form, as returned by most of the Steam API,
        /// to the matching Dota 2 account id.
        /// </summary>
        /// <param name="steamId64">The 64 bit steam id.</param>
        /// <param name="accountId">The 32 bit account id, when the conversion succeeds.</param>
        /// <returns><c>true</c> when the conversion succeeds; otherwise, <c>false</c>.</returns>
        public static bool TryGetAccountId(string steamId64, out uint accountId)
        {
            accountId = 0;
            if (!long.TryParse(steamId64, out long parsed))
            {
                return false;
            }
            long converted = parsed - SteamId64Base;
            if (converted < 0 || converted > uint.MaxValue)
            {
                return false;
            }
            accountId = (uint)converted;
            return true;
        }

        /// <summary>
        /// Gets the team a player slot belongs to.
        /// </summary>
        /// <param name="playerSlot">The player slot as reported by the Dota 2 API.</param>
        /// <returns>The team the player slot belongs to.</returns>
        public static Dota2Team GetTeam(int playerSlot)
        {
            return (playerSlot & TeamBit) == 0 ? Dota2Team.Radiant : Dota2Team.Dire;
        }

        /// <summary>
        /// Gets the zero based position of a player slot within its team, 0 through 4.
        /// </summary>
        /// <param name="playerSlot">The player slot as reported by the Dota 2 API.</param>
        /// <returns>The position of the player slot within its team.</returns>
        public static int GetSlot(int playerSlot)
        {
            return playerSlot & SlotBits;
        }
    }
}
