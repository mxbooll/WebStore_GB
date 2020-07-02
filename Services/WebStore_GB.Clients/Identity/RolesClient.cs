using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using WebStore_GB.Clients.Base;
using WebStore_GB.Domain;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.Interfaces.Services.Identity;

namespace WebStore_GB.Clients.Identity
{
    public class RolesClient : BaseClient, IRolesClient
    {
        public RolesClient(IConfiguration configuration) : base(configuration, WebApi.Identity.ROLES) { }

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
