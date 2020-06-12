using Microsoft.AspNetCore.Identity;

namespace WebStore_GB.Domain.Entities.Identity
{
    public class User : IdentityRole
    {
        public const string ADMINISTRATOR = "Admin";
        public const string DEFAULTADMINPASSWORD = "AdminPAssword";
    }
}
