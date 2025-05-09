using Microsoft.AspNetCore.Identity;
using Volo.Abp.Domain.Entities;

namespace PopcornHub.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public User() {}
}