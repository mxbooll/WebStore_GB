using System;

namespace WebStore_GB.Domain.DTO.Identity
{
    /// <summary>
    /// Модель о передаче информации о времени блокировки, если она установлена для ползователя.
    /// </summary>
    public class SetLockoutDTO : UserDTO
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
