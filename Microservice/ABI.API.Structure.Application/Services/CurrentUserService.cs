using ABI.API.Structure.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace ABI.API.Structure.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private const string nameClaimType = "name";
        private const string idClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        private readonly IHttpContextAccessor _httpContextAccessor;

        private ClaimsPrincipal _user => _httpContextAccessor.HttpContext.User;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public string UserName => _user.FindFirst(nameClaimType)?.Value.ToUpper();

        public Guid UserId
        {
            get
            {
                var claimValue = _user.FindFirst(idClaimType)?.Value;
                return string.IsNullOrEmpty(claimValue) ? default : new Guid(claimValue);
            }
        }
    }
}
