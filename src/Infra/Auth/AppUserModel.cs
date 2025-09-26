// src\Infra\Auth\AppUserModel.cs

using Microsoft.AspNetCore.Identity;

namespace Infra.Auth;

public sealed class AppUser : IdentityUser<Guid>
{
    public required Guid MemberId { get; set; }
}