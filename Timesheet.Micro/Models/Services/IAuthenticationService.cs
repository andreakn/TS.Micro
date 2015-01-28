namespace Timesheet.Micro.Models.Services
{
    public interface IAuthenticationService
    {
        bool Authenticate(string userName, string password);
    }
}