using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore_GB.Domain;
using WebStore_GB.Domain.Entities.Identity;
using WevStore_GB.DAL.Context;

namespace WebStore_GB.ServiceHosting.Controllers
{
    [Route(WebApi.Identity.ROLES)]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role> _roleStore;

        public RolesApiController(WebStoreDB db)
        {
            _roleStore = new RoleStore<Role>(db);
        }
    }
}
