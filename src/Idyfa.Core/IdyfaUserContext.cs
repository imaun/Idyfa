using Idyfa.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Idyfa.Core
{
    public class IdyfaUserContext : IIdyfaUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdyfaUserContext(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor 
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            Principal = _httpContextAccessor.HttpContext.User;
            IsAuthenticated = Principal?.Identity?.IsAuthenticated ?? false;
        }

        public bool IsAuthenticated { get; }
        public Guid UserId => Principal?.Identity?.GetUserId() ?? default;

        public string? DisplayName => Principal?.Identity?.GetUserDisplayName() ?? null;

        public string? PhoneNumber => Principal?.Identity?.GetUserPhoneNumber() ?? null;

        public string? UserName => Principal?.Identity?.GetUserName() ?? null;

        public string? ReferalCode => Principal?.Identity?.GetReferalCode() ?? null;

        public UserStatus? Status => throw new NotImplementedException();

        public ClaimsPrincipal Principal { get; }

        public bool? PhoneNumberConfirmed => Principal?.Identity?.GetUserPhoneNumberConfirmed() ?? null;

        public string? Email => Principal?.Identity?.GetUserEmail() ?? null;
    }
}
