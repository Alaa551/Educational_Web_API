using API_Timono_App.Mapping;
using API_Timono_App.Repository.Abstraction;
using API_Timono_App.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedData.Data;
using SharedData.Data.Models;
using SharedData.DTO;

namespace API_Timono_App.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

    public AuthController(IAuthRepository authRepository,ITokenService tokenService, UserManager<Account> userManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authRepository = authRepository;
    }


    [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            // check if email already exists
            if (await  _authRepository.IsUserExists(registerDto.Email))
            {
                return BadRequest("Email Already Exists");
            }

            // map dto to account and student
            var account = registerDto.ToAccount();
            var student = registerDto.ToStudent(account.Id);

            try
            {
                var registedAccount = await _authRepository.Register(account, registerDto.Password, student);
                return Ok("registeration success");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Validation", ex.Message);
                return BadRequest(ExtractErrorMessages());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return StatusCode(500, ExtractErrorMessages());
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                //if (!await _authRepository.Login(loginDto.Email, loginDto.Password))
                //{
                //    return BadRequest($"incorrect Email or Password: {loginDto.Email} {loginDto.Password}");
                //}

                if (await _authRepository.Login(loginDto.Email, loginDto.Password))
                {

                    var account = await _userManager.FindByEmailAsync(loginDto.Email);

                    string token = await _tokenService.GenerateToken(account);
                    return Ok($"Login success: {token}");
                }
                return StatusCode(500, "incorrect email or password");

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Validation", ex.Message);
                return BadRequest(ExtractErrorMessages());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return StatusCode(500, ExtractErrorMessages());
            }

        }

        private IEnumerable<string> ExtractErrorMessages()
        {
            return ModelState
                .Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }


}
