using FT_Aranda.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Aranda.SQLReposity.Workers
{
    public class UserWorker : IUserWorker
    {
        // Globals 
        private readonly FTArandaDBContext context;

        public UserWorker(FTArandaDBContext context)
        {
            // Dependency Injection
            this.context = context;
        }

        #region Methods

        public async Task<IEnumerable<User>> GetUsers()
        {
            var Users = await Task.Run(() => context.User.OrderBy(row => row.LastName));
            return Users;
        }

        public async Task<User> GetUserByID(int id)
        {
            var User = await context.User.FirstOrDefaultAsync(row => row.Id == id);
            return User;
        }

        public async Task<User> GetUserLikeFirtsName(string firtsName)
        {
            var User = await context.User.FirstOrDefaultAsync(row => row.FirtsName.Contains(firtsName));
            return User;
        }

        public async Task<User> GetUserLikeLastName(string lastName)
        {
            var User = await context.User.FirstOrDefaultAsync(row => row.FirtsName.Contains(lastName));
            return User;
        }

        public async Task<bool> CreateUser(User User)
        {
            await context.User.AddAsync(User);
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> CreateMasiveUsers(List<User> Users)
        {
            await context.AddRangeAsync(Users);
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> UpdateUser(User User)
        {
            await Task.Run(() => context.User.Update(User));
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> DeleteUser(User User)
        {
            await Task.Run(() => context.User.Remove(User));
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> DeleteMasivUsers(List<User> Users)
        {
            await Task.Run(() => context.User.RemoveRange(Users));
            return context.SaveChanges() >= 0;
        }

        #endregion
    }
}
