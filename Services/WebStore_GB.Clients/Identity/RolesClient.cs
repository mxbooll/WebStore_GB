﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
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

        #region IRoleStore<Role>

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync(_serviceAddress, role, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancel) =>
            await (await PutAsync(_serviceAddress, role, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_serviceAddress}/Delete", role, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_serviceAddress}/GetRoleId", role, cancel))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_serviceAddress}/GetRoleName", role, cancel))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task SetRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            role.Name = name;
            await PostAsync($"{_serviceAddress}/SetRoleName/{name}", role, cancel);
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_serviceAddress}/GetNormalizedRoleName", role, cancel))
               .Content
               .ReadAsAsync<string>(cancel);

        public async Task SetNormalizedRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            role.NormalizedName = name;
            await PostAsync($"{_serviceAddress}/SetNormalizedRoleName/{name}", role, cancel);
        }

        public async Task<Role> FindByIdAsync(string id, CancellationToken cancel) =>
            await GetAsync<Role>($"{_serviceAddress}/FindById/{id}", cancel);

        public async Task<Role> FindByNameAsync(string name, CancellationToken cancel) =>
            await GetAsync<Role>($"{_serviceAddress}/FindByName/{name}", cancel);

        #endregion 
    }
}
