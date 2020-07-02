using System.Security.Claims;

namespace WebStore_GB.Domain.DTO.Identity
{
    public class ReplaceClaimDTO : UserDTO 
    {
        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }
    }
}
