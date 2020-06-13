using Microsoft.AspNetCore.Identity;

namespace WebStore_GB.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string ADMINISTRATOR = "Admin";
        public const string DEFAULTADMINPASSWORD = "AdminPAssword";
    }
}
