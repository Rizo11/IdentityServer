using Microsoft.AspNetCore.Identity;

namespace identity.Entity;

public class User : IdentityUser
{
    public string Fullname { get; set; }
    public DateTimeOffset Birthdate { get; set; }
    public virtual ICollection<IdentityRole<Guid>> Roles { get; set; }
    
}