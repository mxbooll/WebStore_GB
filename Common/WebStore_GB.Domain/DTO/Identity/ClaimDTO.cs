using System.Collections.Generic;
using System.Security.Claims;

namespace WebStore_GB.Domain.DTO.Identity
{
    public abstract class ClaimDTO : UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}
