namespace Timesheet.Micro.Models.Services
{
    public interface ICryptographer
    {
        string CreateSalt();
        string ComputeHash(string valueToHash);
        string GetPasswordHash(string password, string salt);
    }
}