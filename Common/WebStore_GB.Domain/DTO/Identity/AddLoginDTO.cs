using Microsoft.AspNetCore.Identity;

namespace WebStore_GB.Domain.DTO.Identity
{
    public class AddLoginDTO : UserDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}
