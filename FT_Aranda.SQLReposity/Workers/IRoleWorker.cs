using FT_Aranda.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FT_Aranda.SQLReposity.Workers
{
    public interface IRoleWorker
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRoleByID(int id);
        Task<Role> GetRoleByName(string name);
        Task<Role> GetRoleLikeName(string name);
        Task<bool> CreateRole(Role role);
        Task<bool> CreateMasiveRoles(List<Role> roles);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(Role role);
        Task<bool> DeleteMasivRoles(List<Role> roles);
    }
}
