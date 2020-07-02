using FT_Aranda.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FT_Aranda.SQLReposity.Workers
{
    public interface IUserWorker
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByID(int id);
        Task<User> GetUserLikeFirtsName(string firtsName);
        Task<User> GetUserLikeLastName(string lastName);
        Task<bool> CreateUser(User User);
        Task<bool> CreateMasiveUsers(List<User> Users);
        Task<bool> UpdateUser(User User);
        Task<bool> DeleteUser(User User);
        Task<bool> DeleteMasivUsers(List<User> Users);
    }
}
