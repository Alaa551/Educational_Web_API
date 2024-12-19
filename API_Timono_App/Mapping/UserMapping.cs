using SharedData.Data.Models;
using SharedData.DTO;

namespace API_Timono_App.Mapping
{
    public static class UserMapping
    {
          public static Account ToAccount(this RegisterDto registerDto)
        {
            var account = new Account
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
            };
             return account;
        }
    

    public static Student ToStudent(this RegisterDto registerDto,string accountId)
    {
        var student = new Student
        {
             Name = registerDto.UserName,
             AccountId = accountId,
             
        };
        return student;
    }
}
}

