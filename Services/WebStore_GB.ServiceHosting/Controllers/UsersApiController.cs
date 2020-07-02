using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore_GB.Domain;
using WebStore_GB.Domain.Entities.Identity;
using WevStore_GB.DAL.Context;

namespace WebStore_GB.ServiceHosting.Controllers
{
    [Route(WebApi.Identity.USERS)]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _userStore;

        public UsersApiController(WebStoreDB db)
        {
            _userStore = new UserStore<User, Role, WebStoreDB>(db);
        }
    }
}
