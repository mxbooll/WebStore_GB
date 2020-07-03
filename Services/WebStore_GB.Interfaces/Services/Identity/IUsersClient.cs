using Microsoft.AspNetCore.Identity;
using WebStore_GB.Domain.Entities.Identity;

namespace WebStore_GB.Interfaces.Services.Identity
{
    /// <summary>
    /// Задача интерфейса, агрегация других интерфейсов.
    /// </summary>
    public interface IUsersClient : 
        IUserRoleStore<User>, 
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserTwoFactorStore<User>, // Хранилище информации двухфакторной авторизации
        IUserClaimStore<User>, // Хранилище дополнительной информации, которая может прикладываться к любому пользователю, либо роли
        IUserLoginStore<User> // Хранилище информации фактов входа пользователя в систему
    {
    }
}
