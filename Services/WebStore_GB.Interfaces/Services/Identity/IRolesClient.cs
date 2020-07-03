using Microsoft.AspNetCore.Identity;
using WebStore_GB.Domain.Entities.Identity;

namespace WebStore_GB.Interfaces.Services.Identity
{
    public interface IRolesClient : IRoleStore<Role>
    {
    }
}
