using FT_Aranda.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FT_Aranda.SQLReposity.Workers
{
    public interface ILoginWorker
    {
        Task<Login> GetLoginInfo(string userName);
        Task<User> Auhenticate(string userName, string password);
        Task<bool> CreateorUpdateLoginInfo(string userName, bool correctAccess);
    }
}
