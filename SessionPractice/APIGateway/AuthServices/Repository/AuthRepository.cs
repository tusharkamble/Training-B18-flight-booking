using AuthServices.DbContextDAO;
using AuthServices.Interfaces;
using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServices.Repository
{
    public class AuthRepository: IAuthRepository
    {
        private ACSDbContext _ACSDbContext;
        public AuthRepository(ACSDbContext dbContext)
        {
            _ACSDbContext = dbContext;
        }
        
        public User GetUserDetailByCredentials(string emailId="",string password="")
        {
            var user = this._ACSDbContext.UserACS
                .Where(u => u.EmailId == emailId && u.Password == password && u.IsActive)
                .FirstOrDefault();
            return user;
        }
    }
}
