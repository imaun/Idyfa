using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Idyfa.Core
{

    public static class IdentityExts
    {

        public static Guid GetUserId(this IIdentity identity) {
            var value = identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException("UserId claim not found.");

            if (Guid.TryParse(value, out Guid userId))
                return userId;

            throw new InvalidOperationException("UserId value is not a valid GUID.");
        }


        public static string? GetUserClaimValue(this IIdentity identity, string claimType) 
            => (identity as ClaimsIdentity)?.FindFirstValue(claimType) ?? null;

        public static string? FindFirstValue(this ClaimsIdentity identity, string claimType)
           => identity?.FindFirst(claimType)?.Value ?? null;

        public static string? GetUserDisplayName(this IIdentity identity)
            => identity
                    ?.GetUserClaimValue(IdyfaUserClaim.DisplayName)
                    ?? identity?.GetUserFullName();

        public static string GetUserFullName(this IIdentity identity)
            => $"{GetUserFirstName(identity)} {GetUserLastName(identity)}";

        public static string? GetUserFirstName(this IIdentity identity)
            => identity?.GetUserClaimValue(ClaimTypes.GivenName);

        public static string? GetUserLastName(this IIdentity identity)
            => identity?.GetUserClaimValue(ClaimTypes.Surname) ?? null;

        public static T? GetUserId<T>(this IIdentity identity) where T : IConvertible {
            var firstValue = identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
            return firstValue != null
                ? (T)Convert.ChangeType(firstValue, typeof(T), CultureInfo.InvariantCulture)
                : default;
        }

        public static UserStatus? GetUserStatus(this IIdentity identity) {
            if (identity is null) return null;
            var claimValue = identity.GetUserClaimValue(IdyfaUserClaim.UserStatus);
            if(claimValue is null) return null;
            return (UserStatus)Convert.ToInt32(claimValue);
        }

        public static bool? GetUserPhoneNumberConfirmed(this IIdentity identity) {
            if(identity is null) return null;
            var claimValue = identity.GetUserClaimValue(IdyfaUserClaim.PhoneNumberConfirmed);
            if (claimValue is null) return null;
            return Convert.ToBoolean(claimValue);
        }

        public static string? GetUserName(this IIdentity identity)
            => identity?.GetUserClaimValue(ClaimTypes.Name) ?? null;

        public static string? GetUserEmail(this IIdentity identity)
            => identity?.GetUserClaimValue(ClaimTypes.Email) ?? null;

        public static string? GetReferalCode(this IIdentity identity)
            => identity?.GetUserClaimValue(IdyfaUserClaim.ReferalCode) ?? null;

        public static string? GetUserPhoneNumber(this IIdentity identity)
            => identity?.GetUserClaimValue(IdyfaUserClaim.PhoneNumber) ?? null;
    }
}
