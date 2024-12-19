namespace API_Timono_App.Validation
{
    using System.Text.RegularExpressions;

    public class ValidationService : IValidationService
    {
        public void ValidateRegisterInput(string email, string password, string username,string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new ArgumentException("Invalid email format.");

            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Trim().Length != 11 || !phoneNumber.All(char.IsDigit))
                throw new ArgumentException("phone is required and must be 11 number");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username is required.");

        }

        public void ValidateLoginInput(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new ArgumentException("Invalid email format.");

        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }
    }

}
