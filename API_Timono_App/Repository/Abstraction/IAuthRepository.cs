using SharedData.Data.Models;

namespace API_Timono_App.Repository.Abstraction
{
    public interface IAuthRepository
    {
        Task<bool> IsUserExists(string email);
        Task<Account> Register(Account account, string password, Student student);
        Task<bool> Login(string email, string password);
    }
}
