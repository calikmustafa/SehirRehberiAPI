using Microsoft.EntityFrameworkCore;
using SehirRehberiAPI.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberiAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string UserName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
            if (user==null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userpasswordHash, byte[] userpasswordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512(userpasswordSalt))
            {
                var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=userpasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordsalt, out byte[] passwordHash)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public  async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordsalt;
            CreatePasswordHash(password, out passwordsalt, out passwordHash);

            user.PasswordSalt = passwordsalt;
            user.PasswordHash = passwordHash;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<bool> UserExist(string UserName)
        {
            if (await _context.Users.AnyAsync(x=>x.UserName==UserName))
            {
                return true;
            }
            return false;
        }
    }
}
