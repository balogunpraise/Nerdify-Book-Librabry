using Microsoft.AspNetCore.Identity;
using System;

namespace Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
