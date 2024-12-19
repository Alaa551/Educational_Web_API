using System;


using Microsoft.AspNetCore.Identity;

namespace SharedData.Data.Models
{
    public class Account : IdentityUser
    {
        public string? FullName { get; set; }
        public Student? Student { get; set; }
    }
}
