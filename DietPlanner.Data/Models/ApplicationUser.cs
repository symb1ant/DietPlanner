using Microsoft.AspNetCore.Identity;

namespace DietPlanner.Data.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<DietEntry> DietEntries { get; set; }
}

