using System.Security.Claims;

namespace Idyfa.Core.Contracts
{
    public interface IIdyfaUserContext
    {
        ClaimsPrincipal Principal { get; }

        bool IsAuthenticated { get; }

        Guid UserId { get; }

        string? DisplayName { get; }

        string? PhoneNumber { get; }

        bool? PhoneNumberConfirmed { get; }

        string? UserName { get; }

        string? Email { get; }

        string? ReferalCode { get; }

        UserStatus? Status { get; }

    }
}
