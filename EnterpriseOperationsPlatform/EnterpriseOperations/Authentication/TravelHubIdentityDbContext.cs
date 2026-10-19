using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseOperations.Authentication;

public class TravelHubIdentityDbContext
    : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public TravelHubIdentityDbContext(
        DbContextOptions<TravelHubIdentityDbContext> options)
        : base(options)
    {
    }
}