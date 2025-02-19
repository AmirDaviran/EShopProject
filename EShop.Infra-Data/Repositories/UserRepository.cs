using EShop.Domain.Entities.Account;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EShop.Infra_Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Constractor

        private readonly EShopDbContext _context;
        public UserRepository(EShopDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Account

        public async Task<User> GetUserById(int id)
        {
           return await _context.Users.SingleOrDefaultAsync(u => u.Id  == id);
        }

        public async Task<bool> IsExistsUserByEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email.Trim().ToLower());
        }

        public async Task<User> GetUserByEmail(string email)
        {
            //string trimmedEmail = FixedText.Fix
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email.Trim().ToLower());
            return user;
        }

        public async Task<User> GetUserByActiveCode(string code)
        {
            return await _context.Users.SingleOrDefaultAsync(ua => ua.ActiveCode == code);
        }


        public async Task CreateUser(User user)
        {
           await _context.Users.AddAsync(user);

        }

        public async void  UpdateUser(User user)
        {
             _context.Users.Update(user);
            

        }

        public async Task DeleteUser(int id)
        {
            User user = await GetUserById(id);
            user.IsDeleted = true;
            UpdateUser(user);

        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

       

        #endregion

        #region Admin

        public async Task<bool> DuplicatedEmailAsync(int id, string email)
        {
            return await _context.Users
                .AnyAsync(user => user.Email == email && user.Id != id);
        }

        public async Task<bool> DuplicatedMobileAsync(int id, string mobile)
        {
            return await _context.Users
                .AnyAsync(user => user.PhoneNumber == mobile && user.Id != id);
        }


        public async Task<User> GetUserByEmailForAdminAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetUserByIdForAdminAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task InsertUserByAdminAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public IQueryable<User> GetUsersAsQueryable()
        {
            return _context.Users.AsQueryable();
        }
        #endregion
    }
}
