using EShop.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        #region Account
        Task<User> GetUserById(int id);
        Task<bool> IsExistsUserByEmail(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByActiveCode(string code);
        Task CreateUser(User user);
        void UpdateUser(User user);
        Task DeleteUser(int id);
        Task SaveChangesAsync();
        #endregion

        #region Admin

        Task<bool> DuplicatedEmailAsync(int id, string email);
        Task<bool> DuplicatedMobileAsync(int id, string mobile);
        IQueryable<User> GetUsersAsQueryable();

        //Task<(List<User>, int)> FilterUsersAsync(string? firstName, string? lastName, string? email, string? phoneNumber, bool? isActive, int skip, int take);

        #endregion
    }
}
