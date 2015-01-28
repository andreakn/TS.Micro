using Timesheet.Micro.Data.Repos;

namespace Timesheet.Micro.Models.Services
{
    public class DatabaseAuthenticationService : IAuthenticationService
    {
        private readonly ICryptographer _cryptographer;
        private readonly IUserRepository _userRepository;

        public DatabaseAuthenticationService(ICryptographer cryptographer, IUserRepository userRepository)
        {
            _cryptographer = cryptographer;
            _userRepository = userRepository;
        }

        public bool Authenticate(string userName, string password)
        {
            var user = _userRepository.GetByUserName(userName);
            var passwordHash = _cryptographer.GetPasswordHash(password, user.PasswordSalt);
            return passwordHash.Equals(user.PasswordHash);
        }
    }
}
