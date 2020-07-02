using FT_Aranda.Models;
using FT_Aranda.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Aranda.SQLReposity.Workers
{
    public class RoleWorker : IRoleWorker
    {
        // Globals 
        private readonly FTArandaDBContext context;

        public RoleWorker(FTArandaDBContext context)
        {
            // Dependency Injection
            this.context = context;
        }

        #region Methods

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await Task.Run(() => context.Role.OrderBy(row => row.Name));
            return roles;
        }

        public async Task<Role> GetRoleByID(int id)
        {
            var role = await context.Role.FirstOrDefaultAsync(row => row.Id == id);
            return role;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            var role = await context.Role.FirstOrDefaultAsync(row => row.Name == name);
            return role;
        }

        public async Task<Role> GetRoleLikeName(string name)
        {
            var role = await context.Role.FirstOrDefaultAsync(row => row.Name.Contains(name));
            return role;
        }

        public async Task<bool> CreateRole(Role role)
        {
            await context.Role.AddAsync(role);
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> CreateMasiveRoles(List<Role> roles)
        {
            await context.AddRangeAsync(roles);
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> UpdateRole(Role role)
        {
            await Task.Run(() => context.Role.Update(role));
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> DeleteRole(Role role)
        {
            await Task.Run(() => context.Role.Remove(role));
            return context.SaveChanges() >= 0;
        }

        public async Task<bool> DeleteMasivRoles(List<Role> roles)
        {
            await Task.Run(() => context.Role.RemoveRange(roles));
            return context.SaveChanges() >= 0;
        }

        #endregion
    }
}
