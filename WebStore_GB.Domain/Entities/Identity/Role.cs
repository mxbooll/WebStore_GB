using Microsoft.AspNetCore.Identity;

namespace WebStore_GB.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string ADMINISTRATOR = "Administrators";
        public const string USER = "Users";
    }
}
