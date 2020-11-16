namespace Services.Abstraction
{
    public interface ILogInService
    {
        public bool AuthenticateUser(string UserId, string Password);
    }
}
