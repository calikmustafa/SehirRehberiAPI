using SehirRehberiAPI.Model;
using System.Threading.Tasks;

namespace SehirRehberiAPI.Data
{
    public interface IAuthRepository
    {
        Task<User>Register(User user,string password);
        Task<User> Login(string UserName,string password);
        Task<bool> UserExist(string UserName);

    }
}
