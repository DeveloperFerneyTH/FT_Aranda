using FT_Aranda.Models;
using FT_Aranda.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FT_Aranda.SQLReposity.Workers
{    
    public class LoginWorker : ILoginWorker
    {
        // Globals 
        private readonly FTArandaDBContext context;
        private readonly CustomSettings customSettings;

        public LoginWorker(FTArandaDBContext context, IOptions<CustomSettings> options)
        {
            // Dependency Injection
            this.context = context;
            customSettings = options.Value;
        }

        #region Methods

        public async Task<Login> GetLoginInfo(string userName)
        {
            var login = await context.Login.FirstOrDefaultAsync(row => row.UserName == userName.ToUpper());
            return login;
        }

        public async Task<User> Auhenticate(string userName, string password)
        {
            var user = await context.User.Include(row => row.Role).FirstOrDefaultAsync(row => row.UserName == userName.ToUpper() && row.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(customSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public async Task<bool> CreateorUpdateLoginInfo(string userName, bool correctAccess)
        {
            var loginInfo = await GetLoginInfo(userName);

            if (loginInfo == null)
            {
                Login login = new Login()
                {
                    Attemps = 0,
                    Blocked = false,
                    LastEntryDate = DateTime.Now,
                    UserName = userName.ToUpper()
                };

                await context.Login.AddAsync(login);
                return await context.SaveChangesAsync() >= 0;
            }
            else
            {
                int attemps = correctAccess ? 0 : loginInfo.Attemps + 1;
                bool blocked = attemps > 3 ? true : false;

                var loginEdit = await context.Login.FirstOrDefaultAsync(row => row.UserName == userName.ToUpper());

                loginEdit.Attemps = attemps;
                loginEdit.Blocked = blocked;
                loginEdit.LastEntryDate = DateTime.Now;
                return await context.SaveChangesAsync() >= 0;
            }
        }

        #endregion
    }
}
