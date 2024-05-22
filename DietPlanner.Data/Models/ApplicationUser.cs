using Microsoft.AspNetCore.Identity;

namespace DietPlanner.Data.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<DietEntry> DietEntries { get; set; }
}

