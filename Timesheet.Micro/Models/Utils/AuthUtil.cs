using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timesheet.Micro.Data.Repos;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Services;

namespace Timesheet.Micro.Models.Utils
{
    public class AuthUtil
    {
        public const string DEFAULT_PASSWORD = "ChangeMeASAP!";

        private ICryptographer crypto;
        private IUserRepository userRepo;
        public AuthUtil(ICryptographer crypto, IUserRepository userRepo)
        {
            this.crypto = crypto;
            this.userRepo = userRepo;
        }

        public bool Authenticate(string userName, string password)
        {
            if (!userRepo.UsernameExists(userName)) return false;
            var user = userRepo.GetByUserName(userName);
            var passwordHash = crypto.GetPasswordHash(password, user.PasswordSalt);
            return passwordHash.Equals(user.PasswordHash);
        }

        public User CreateUser(string username, string password)
        {
            var user = new User();
            user.Username = username;
            user.PasswordSalt = crypto.CreateSalt();
            user.PasswordHash = crypto.GetPasswordHash(password, user.PasswordSalt);
            return user;
        }
       
        public void SetPassword(User user, string password)
        {
            user.PasswordSalt = crypto.CreateSalt();
            user.PasswordHash = crypto.GetPasswordHash(password, user.PasswordSalt);
        }
    }
}