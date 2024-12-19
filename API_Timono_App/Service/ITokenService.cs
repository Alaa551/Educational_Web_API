using SharedData.Data.Models;

namespace API_Timono_App.Service
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(Account account);
    }
}
