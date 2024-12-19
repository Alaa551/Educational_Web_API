using API_Timono_App.Repository.Abstraction;
using API_Timono_App.Validation;
using Microsoft.AspNetCore.Identity;
using SharedData.Data;
using SharedData.Data.Models;

namespace API_Timono_App.Repository.Implementation
{
    public class AuthRepositoryImp : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly IValidationService _validationService;

        public AuthRepositoryImp(AppDbContext context, UserManager<Account> userManager, IValidationService validationService)
        {
            _validationService = validationService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> IsUserExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<bool> Login(string email, string password)
        {
            _validationService.ValidateLoginInput(email, password);

            var account = await _userManager.FindByEmailAsync(email);

            if (account == null)
                throw new Exception("account email not found");

            var checkPassword = await _userManager.CheckPasswordAsync(account, password);
            if (!checkPassword)
                throw new Exception("incorrect password");

            return checkPassword;
        }

        public async Task<Account> Register(Account account, string password, Student student)
        {
            _validationService.ValidateRegisterInput(account.Email, password, account.UserName, account.PhoneNumber);

            var res = await _userManager.CreateAsync(account, password);
            if (res.Succeeded)
            {
                student.AccountId = account.Id;
                student.Name = account.UserName;
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return account;
            }

            var errorMsg = string.Join(',', res.Errors.Select(e => e.Description));
            throw new Exception($"register failed : {errorMsg}");

        }


    }
}
