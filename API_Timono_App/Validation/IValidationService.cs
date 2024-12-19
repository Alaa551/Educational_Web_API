namespace API_Timono_App.Validation
{
    public interface IValidationService
    {
        void ValidateRegisterInput(string email, string password, string username,string phoneNumber);
        void ValidateLoginInput(string email, string password);
    }

}
